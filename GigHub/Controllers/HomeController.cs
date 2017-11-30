using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using GigHub.ViewModel;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
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

            var viewModel = new GigsViewModel
            {
                UpComingGigs = upComingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Home",
                SearchTerm = query
            };

            return View("Gigs",viewModel);
        }

    

    }
}