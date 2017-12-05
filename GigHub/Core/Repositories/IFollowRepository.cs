using GigHub.Core.Models;
using System;

namespace GigHub.Repositories
{
    public interface IFollowRepository
    {
        Following GetFollowing(String followerId, String followeeId);
        void Remove(Following following);
        void Add(Following following);
    }
}