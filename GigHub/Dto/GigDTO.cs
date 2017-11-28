using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Dto
{
    public class GigDTO
    {
        public int Id { get; set; }
        public bool IsCanceled { get; set; }

        public UserDTO Artist { get; set; }
        public DateTime DateTime { get; set; }
        public String Venue { get; set; }
        public GenreDTO Genre { get; set; }
    
    }
}