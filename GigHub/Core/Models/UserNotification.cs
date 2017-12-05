using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GigHub.Core.Models
{
    public class UserNotification
    {
        // Should Always  create default  for entity
        protected UserNotification(){ }

        public UserNotification(ApplicationUser user, Notification notification)
        {
            if (user == null || notification == null)
                throw new ArgumentNullException("User or notification should not be null");
                 
            Notification = notification;
            User = user;
        }
     
        [Key]
        [Column(Order=1)]
        public String UserId { get;private set; }
        [Key]
        [Column(Order = 2)]
        public int NotificationId { get;private set; }

        //navigation properties
        public ApplicationUser User { get; private set; }
        public Notification Notification { get; private set; }

        public bool IsRead { get; set; }

        public void Read()
        {
            IsRead = true;
        }

    }
}