﻿using GigHub.Dto;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private ApplicationDbContext _context;
        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();

            var exists = _context.Followings.Any(f => f.FollowerId == userId && f.FolloweeId == dto.FolloweeId);

            if (exists)
                return BadRequest("Following already exists");


            var following = new Following
            {
                FollowerId = userId,
                FolloweeId = dto.FolloweeId
            };

            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok();
        }

        //Delete Follwing 
        [HttpDelete]
        public IHttpActionResult DeleteFollowing(FollowingDto dto)
        {
            var userId = User.Identity.GetUserId();
            var follow = _context.Followings.SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == dto.FolloweeId);
         

            if (follow == null)
                return BadRequest("the follow doesnt exist");
            
          
            _context.Followings.Remove(follow);
            _context.SaveChanges();

            return Ok();
            }

    }
}
