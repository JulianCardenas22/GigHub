using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace GigHub.Models
{
    public class Notification
    {
       
        public int Id { get; private set; }
        public DateTime DateTime { get; private set; }
        public NotificationType Type { get;private  set; }
        public DateTime? OriginalDateTime { get;private set; }
        public String OriginalVenue { get;private set; }

        [Required]
        public Gig Gig { get;private set; }

        // Default ctor for entity
        protected Notification(){   
        }

        private Notification(Gig gig, NotificationType notificationType)
        {
            Gig = gig ?? throw new ArgumentNullException("gig should not be null");
            Type = notificationType;
            DateTime = DateTime.Now;
        }

        public static Notification GigCreated(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCreated); 
        }
        public static Notification GigUpdated(Gig  newGig, DateTime originalDateTime, String originalVenue)
        {

           var notification = new Notification(newGig, NotificationType.GigUpdated);
            notification.OriginalDateTime = originalDateTime;
            notification.OriginalVenue = originalVenue;
            return notification;
        }
        public static Notification GigCanceled(Gig gig)
        {
            return new Notification(gig, NotificationType.GigCanceled);
        }

      
    }
}