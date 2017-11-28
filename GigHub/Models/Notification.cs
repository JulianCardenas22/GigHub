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
        public DateTime? OriginalDateTime { get; set; }
        public String OriginalVenue { get; set; }

        [Required]
        public Gig Gig { get;private set; }

        // Default ctor for entity
        protected Notification(){   
        }

        public Notification(Gig gig, NotificationType notificationType)
        {
            if (gig == null)
                throw new ArgumentNullException("gig should not be null");

            Gig = gig;
            Type = notificationType;
            DateTime = DateTime.Now;
        }
    }
}