using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.ViewModel
{
    public class GigsViewModel
    {
        public IEnumerable<Gig> UpComingGigs { get; set; }
        public bool ShowActions { get; set; }
        public string Heading { get; set; }
        public String SearchTerm { get; set; }
        public ILookup<int,Attendance> Attendances { get; internal set; }
    }
}