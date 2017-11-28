using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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

            var notification = new Notification(this, NotificationType.GigCanceled);

            foreach (var attendee in Attendances.Select(a => a.Attendee))
            {
                attendee.Notify(notification);
            }

        }

    }
}