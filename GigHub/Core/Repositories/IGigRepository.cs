using GigHub.Core.Models;
using System;
using System.Collections.Generic;

namespace GigHub.Core.Repositories
{
    public interface IGigRepository
    {
        Gig GetGigWithAttendee(int gigId);
        Gig GetGigWithArtistGenre(int id);
        IEnumerable<Gig> GetUpComingGigsByArtist(String userId);
        IEnumerable<Gig> GetGitUserAttendances(String userId);
        void AddGig(Gig gig);
        Gig GetGig(int id);
        IEnumerable<Gig> GetUpCommingGigs(string searchTerm = null);
    }
}