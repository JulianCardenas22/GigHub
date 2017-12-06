using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using GigHub.Core.ViewModel;
using Microsoft.AspNet.Identity;
using GigHub.Core;

namespace GigHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index(String query = null)
        {
            
            var upComingGigs = _unitOfWork.Gigs.GetUpCommingGigs(query);
           


            GigsViewModel viewModel;

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();

                var attendances = _unitOfWork.Attendances.GetFutureAttendances(userId).ToLookup(a => a.GigId);
             

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