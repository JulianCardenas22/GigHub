using GigHub.Core.Models;
using GigHub.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IGigRepository Gigs  { get;private set; }
        public IGenreRepository Genres { get;private set; }
        public IFollowRepository Follows { get;private set; }
        public IAttendanceRepository Attendances { get;private set; }

        public UnitOfWork(ApplicationDbContext context){
            _context = context;
            Gigs = new GigRepository(_context);
            Attendances = new AttendanceRepository(context);
            Genres = new GenreRepository(context);
            Follows = new FollowRepository(context);
        }

        public void Complete(){
            _context.SaveChanges();
        }
    }
}