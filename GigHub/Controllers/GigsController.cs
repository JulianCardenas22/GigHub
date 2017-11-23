using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GigHub.Models;
using GigHub.ViewModel;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Data.Entity;

namespace GigHub.Controllers
{

   
    public class GigsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private DateTime _date;

        public GigsController()
        {
             _context = new ApplicationDbContext();
             _date = new DateTime();
            
        }

        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel
            {
                Genres = _context.Genres.ToList()
            };
            
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GigFormViewModel viewModel) {
            if (!ModelState.IsValid)
            {
                viewModel.Genres = _context.Genres.ToList();
                return View("Create", viewModel);
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
            var gigs = _context.Attendances.Where(a => a.AttendeeId == userId)
                                           .Select(a => a.Gig)
                                           .Include(g => g.Artist)
                                           .Include(g => g.Genre)
                                           .ToList();
                                        
               
            var viewModel = new GigsViewModel
            {
                UpComingGigs = gigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading="Gigs I'm Attending"

            };

          
            return View("Gigs",viewModel);
        } 

        [Authorize]
        public ActionResult Mine() {
            
            var userId = User.Identity.GetUserId();
            var gigs = _context.Gigs.Where(g => g.ArtistId == userId && g.DateTime > DateTime.Now)
                                    .Include(g => g.Genre)
                                    .ToList();
            return View(gigs);
        }
       
    }
}