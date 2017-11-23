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

        public ActionResult Index()
        {
            
            var upComingGigs = _context.Gigs
                                            .Include(g => g.Artist)
                                            .Include(g => g.Genre)
                                            .Where(g => g.DateTime > DateTime.Now);


            var viewModel = new GigsViewModel
            {
                UpComingGigs = upComingGigs,
                ShowActions = User.Identity.IsAuthenticated,
                Heading = "Home"
            };

            return View("Gigs",viewModel);
        }

    }
}