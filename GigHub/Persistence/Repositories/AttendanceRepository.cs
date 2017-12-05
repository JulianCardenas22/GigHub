﻿using GigHub.Core.Models;
using GigHub.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GigHub.Persistence.Repositories
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