using GigHub.Models;
using System;


namespace GigHub.Dto
{
    public class NotificationDTO
    {
        
        public DateTime DateTime { get;  set; }
        public NotificationType Type { get;  set; }
        public DateTime? OriginalDateTime { get;  set; }
        public String OriginalVenue { get;  set; }
        public GigDTO Gig { get;  set; }
    }
}