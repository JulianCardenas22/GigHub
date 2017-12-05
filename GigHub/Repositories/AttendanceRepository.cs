using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public  IEnumerable<Attendance> GetFutureAttendances(String userId)
        {
            var futureAttendances = _context.Attendances.Where(
                                     a => a.AttendeeId == userId &&
                                     a.Gig.DateTime > DateTime.Now)
                                    .ToList();

            return futureAttendances;
        }

        public Attendance GetAttendance(String userId, int gigId )
        {
            return _context.Attendances.SingleOrDefault(a => a.GigId == gigId && a.AttendeeId == userId);
        }
       
    }
}