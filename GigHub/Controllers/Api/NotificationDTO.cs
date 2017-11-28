using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Controllers.Api
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