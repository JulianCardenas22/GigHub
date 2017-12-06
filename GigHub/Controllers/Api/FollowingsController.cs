using GigHub.Core;
using GigHub.Core.Dto;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public FollowingsController(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }
      
        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            var exists = _unitOfWork.Follows.GetFollowing(userId, dto.FolloweeId); 
            
            if (exists != null)
                return BadRequest("Following already exists");


            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };


            _unitOfWork.Follows.Add(following);
            _unitOfWork.Complete();

            return Ok();
        }

       
        [HttpDelete]
        public IHttpActionResult UnFollow(string id)
        {
            var userId = User.Identity.GetUserId();
            var follow = _unitOfWork.Follows.GetFollowing(userId, id);
                
         
            if (follow == null)
                return NotFound();


            _unitOfWork.Follows.Remove(follow);
            _unitOfWork.Complete();

            return Ok(id);
            }

    }
}
