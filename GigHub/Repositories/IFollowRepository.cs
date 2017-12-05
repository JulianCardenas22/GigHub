using GigHub.Models;
using System;

namespace GigHub.Repositories
{
    public interface IFollowRepository
    {
        Following GetFollowing(String followerId, String followeeId);
    }
}