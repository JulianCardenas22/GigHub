using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Repositories
{
    public class FollowRepository : IFollowRepository
    {
        private readonly ApplicationDbContext _context;
        public FollowRepository(ApplicationDbContext context )
        {
            _context = context;
        }

        public Following GetFollowing(String followerId, String followeeId)
        {
            return _context.Followings.SingleOrDefault(a => a.FollowerId == followerId && a.FolloweeId == followeeId);
        }
    }
}