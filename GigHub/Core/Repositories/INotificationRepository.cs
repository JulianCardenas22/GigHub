using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Core.Repositories
{
    

        public interface INotificationRepository
        {
            IEnumerable<Notification> GetNewNotificationsFor(string userId);
        }
    
}