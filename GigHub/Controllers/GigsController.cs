using System;
using System.Linq;
using System.Web.Mvc;
using GigHub.Models;
using GigHub.ViewModel;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Data.Entity;
using System.Collections.Generic;
using GigHub.Repositories;

namespace GigHub.Controllers
{

   
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly GigRepository _gigRepository;
        private readonly AttendanceRepository _attendanceRepository;
        private readonly GenreRepository _genreRepository;
        private readonly FollowRepository _followRepository;

        public GigsController()
        {
             _context = new ApplicationDbContext();
            _gigRepository = new GigRepository(_context);
            _attendanceRepository = new AttendanceRepository(_context);
            _genreRepository = new GenreRepository(_context);
            _followRepository = new FollowRepository(_context);
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _genreRepository.GetGenres(),
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
                viewModel.Genres = _genreRepository.GetGenres();
                return View("GigForm", viewModel);
            }

            try { 

            var gig = new Gig
            {
                ArtistId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                GenreId = viewModel.Genre,
                Venue = viewModel.Venue
            };

            _context.Gigs.Add(gig);
            _context.SaveChanges(); }

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
                UpComingGigs = _gigRepository.GetGitUserAttendances(userId),
                ShowActions = User.Identity.IsAuthenticated,
                Heading="Gigs I'm Attending",
                Attendances = _attendanceRepository.GetFutureAttendances(userId).ToLookup(a => a.GigId)
            };

            return View("Gigs",viewModel);
        } 

        [Authorize]
        public ActionResult Mine() {

            var gigs = _gigRepository.GetUpComingGigsByArtist(User.Identity.GetUserId());

            return View(gigs);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
          
            var gig = _gigRepository.GetGig(id);

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
                Genres = _genreRepository.GetGenres()
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
                viewModel.Genres = _genreRepository.GetGenres();
                return View("GigForm", viewModel);
            }

            var gig = _gigRepository.GetGigWithAttendee(viewModel.Id);

            if (gig == null)
                return HttpNotFound();

            if (gig.ArtistId != User.Identity.GetUserId())
                return new HttpUnauthorizedResult();

            gig.Modify(viewModel.GetDateTime(),viewModel.Venue, viewModel.Genre);
            _context.SaveChanges();
   
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

            var gig = _gigRepository.GetGigWithArtistGenre(id);

            if (gig == null)
                return HttpNotFound();



            var viewModel = new GigDetailsViewModel
            {
                Gig = gig,
                IsAttending = _attendanceRepository.GetAttendance(userId, gig.Id) != null,
                IsFollowing = _followRepository.GetFollowing(userId, gig.ArtistId)!= null
            };




            return View(viewModel);
        }


     
    }
}