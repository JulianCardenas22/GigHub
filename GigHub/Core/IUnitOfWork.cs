using GigHub.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Repositories
{
    public interface IUnitOfWork
    {
        IGigRepository Gigs { get; }
        IGenreRepository Genres { get; }
        IFollowRepository Follows { get; }
        IAttendanceRepository Attendances { get; }
        IApplicationUserRepository Users { get; }
        INotificationRepository Notifications { get; }
        IUserNotificationRepository UserNotifications { get; }

        void Complete();
    }

}