using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using GigHub.Core.ViewModel;
using Microsoft.AspNet.Identity;
using GigHub.Repositories;
using GigHub.Persistence;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        private AttendanceRepository _attendanceRepository;

        public HomeController()
        {
            _context = new ApplicationDbContext();
            _attendanceRepository = new AttendanceRepository(_context);
        }

        public ActionResult Index(String query = null)
        {
            
            var upComingGigs = _context.Gigs
                                            .Include(g => g.Artist)
                                            .Include(g => g.Genre)
                                            .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled);


            if (!String.IsNullOrEmpty(query))
            {
                upComingGigs = upComingGigs.Where(g => g.Artist.Name.Contains(query)
                                                    || g.Venue.Contains(query)
                                                    || g.Genre.Name.Contains(query));
            }

            GigsViewModel viewModel;

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var attendances = _attendanceRepository.GetFutureAttendances(userId).ToLookup(a => a.GigId);

                viewModel = new GigsViewModel()
                {
                    UpComingGigs = upComingGigs,
                    ShowActions = User.Identity.IsAuthenticated,
                    Heading = "Home",
                    SearchTerm = query,
                    Attendances = attendances
                };

            }
            else
            {
                viewModel = new GigsViewModel()

                {
                    UpComingGigs = upComingGigs,
                    ShowActions = User.Identity.IsAuthenticated,
                    Heading = "Home",
                    SearchTerm = query,
                 
                };
            }
            return View("Gigs",viewModel);
        }

    

    }
}