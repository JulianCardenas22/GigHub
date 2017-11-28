using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using GigHub.Models;

namespace GigHub.Models
{
    public class Gig
    {

       
        public int Id { get; set; }

        public bool IsCanceled { get; private set; }

        public ApplicationUser Artist { get; set; }

        public DateTime DateTime { get; set; }

        [Required]
        public String ArtistId { get; set; }
          
        [Required]
        [StringLength(255)]
        public String Venue { get; set; }
     
        public Genre Genre { get; set; }

        [Required]
        public byte GenreId { get; set; }

        public ICollection<Attendance> Attendances { get;private set; }

        public Gig()
        {
            Attendances = new Collection<Attendance>();
        }

        public void Cancel() {
            IsCanceled = true;

            var notification =  Notification.GigCanceled(this);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
                attendee.Notify(notification);
            }


        public void Modify(DateTime dt , String venue , byte genre )
        {
            var notification = Notification.GigUpdated(this, dt , venue);
         
            DateTime = dt;
            Venue = venue;
            GenreId = genre;

            foreach (var attendee in Attendances.Select(a => a.Attendee))
                attendee.Notify(notification);
        }

    }
}