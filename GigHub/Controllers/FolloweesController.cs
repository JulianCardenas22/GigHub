
using GigHub.Persistence;
using GigHub.Repositories;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    [Authorize]
    public class FolloweesController : Controller
    {

        private readonly IUnitOfWork  _unitOfWork;

        public FolloweesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
        /*    var artistFollowed = _context.Followings.Where(f => f.FollowerId == userId)
                                            .Select(f=> f.FolloweeId)
                                            .ToList();
*/
            return View( /*var artistFollowed*/);
        }
    }
}