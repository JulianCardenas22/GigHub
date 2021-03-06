﻿using GigHub.Core.Models;
using GigHub.Core.Repositories;
using System;
using System.Linq;


namespace GigHub.Persistence.Repositories


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
        public void Add(Following following)
        {
            _context.Followings.Add(following);
        }

        public void Remove(Following following)
        {
            _context.Followings.Remove(following);
        }
    }
}