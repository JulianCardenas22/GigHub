
using Microsoft.AspNet.Identity;
using System.Web.Http;
using GigHub.Repositories;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class GigsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public GigsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        [HttpDelete]
        public IHttpActionResult Cancel(int id)
        {
            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gigs.GetGigWithAttendee(id);
           
                                 

            if (gig.IsCanceled)
                return NotFound();

            if (gig.ArtistId != userId)
                return Unauthorized();


            gig.Cancel();

            _unitOfWork.Complete();

            return Ok();

        }
       
        /*
        [HttpPut]
        public IHttpActionResult Updated(int id) {
            var userId = User.Identity.GetUserId();
            var gig = _unitOfWork.Gigs.GetGigWithAttendee(id);

            gig.Modify(gig.DateTime,gig.Venue,gig.GenreId);

            return Ok();
        }*/

    }
}
