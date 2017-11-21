using System;
using System.ComponentModel.DataAnnotations;

namespace GigHub.Models
{
    public class Gig
    {
       
        public int Id { get; set; }

 
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
    }
}