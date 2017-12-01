using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Repositories
{
    public class FollowRepository
    {
        private readonly ApplicationDbContext _context;
        public FollowRepository(ApplicationDbContext context )
        {
            _context = context;
        }

        public Following GetFollowing(String userId, String artistId)
        {
            return _context.Followings.SingleOrDefault(a => a.FollowerId == userId && a.FolloweeId == userId);
        }
    }
}