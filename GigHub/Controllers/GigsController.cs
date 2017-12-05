using System;
using System.Linq;
using System.Web.Mvc;
using GigHub.Core.ViewModel;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Data.Entity;
using System.Collections.Generic;
using GigHub.Repositories;
using GigHub.Persistence;
using GigHub.Core.Models;

namespace GigHub.Controllers
{

   
    public class GigsController : Controller
    {
         private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _unitOfWork.Genres.GetGenres(),
                Heading="Add a gig"
            };
            
            return View("GigForm",viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel) {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                return View("GigForm", viewModel);
            }

            try
            {

                var gig = new Gig
                {
                    ArtistId = User.Identity.GetUserId(),
                    DateTime = viewModel.GetDateTime(),
                    GenreId = viewModel.Genre,
                    Venue = viewModel.Venue
                };

                _unitOfWork.Gigs.AddGig(gig);
                _unitOfWork.Complete();
            }

            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }

            return RedirectToAction("Mine", "Gigs");
            
        }

        [Authorize]
        public ActionResult Attending() {
            var userId = User.Identity.GetUserId();

            var viewModel = new GigsViewModel
            {
                UpComingGigs = _unitOfWork.Gigs.GetGitUserAttendances(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading="Gigs I'm Attending",
                Attendances = _unitOfWork.Attendances.GetFutureAttendances(userId).ToLookup(a => a.GigId)
            };

            return View("Gigs",viewModel);
        } 

        [Authorize]
        public ActionResult Mine() {

            var gigs = _unitOfWork.Gigs.GetUpComingGigsByArtist(User.Identity.GetUserId());

            return View(gigs);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
          
            var gig = _unitOfWork.Gigs.GetGig(id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            var viewModel = new GigFormViewModel
            {
                Id = gig.Id,
                Date = gig.DateTime.ToString("d/M/yyyy"),
                Time = gig.DateTime.ToString("HH:mm"),
                Genre= gig.GenreId,
                Venue = gig.Venue,
                Heading = "Edit a gig",
                Genres = _unitOfWork.Genres.GetGenres()
            };

            return View("GigForm", viewModel);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(GigFormViewModel viewModel)
        {
          
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _unitOfWork.Genres.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = _unitOfWork.Gigs.GetGigWithAttendee(viewModel.Id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            gig.Modify(viewModel.GetDateTime(),viewModel.Venue, viewModel.Genre);
            _unitOfWork.Complete();
   
            return RedirectToAction("Mine", "Gigs");
        }

        [HttpPost]
        public ActionResult Search(GigsViewModel viewModel)
        {
            return RedirectToAction("Index", "Home", new { query = viewModel.SearchTerm });
        }

        [Authorize]
        public ActionResult Details(int id )
        {
            var userId = User.Identity.GetUserId();

            var gig = _unitOfWork.Gigs.GetGigWithArtistGenre(id);

            if (gig == null)
                return HttpNotFound();



            var viewModel = new GigDetailsViewModel
            {
                Gig = gig,
                IsAttending = _unitOfWork.Attendances.GetAttendance(userId, gig.Id) != null,
                IsFollowing = _unitOfWork.Follows.GetFollowing(userId, gig.ArtistId)!= null
            };




            return View(viewModel);
        }


     
    }
}