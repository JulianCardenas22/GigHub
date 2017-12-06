using System;


namespace GigHub.Core.Models
{
    public class Attendance
    {
        public Gig Gig { get; set; }
        public ApplicationUser Attendee { get; set; }

     
        public int GigId { get; set; }
        public String AttendeeId { get; set; }
    }
}