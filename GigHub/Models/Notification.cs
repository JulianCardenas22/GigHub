using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace GigHub.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public NotificationType Type { get; set; }
        public DateTime? OriginalDateTime { get; set; }
        public String OriginalVenue { get; set; }

        [Required]
        public Gig Gig { get; set; }
    }
}