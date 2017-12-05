using GigHub.Core.Models;
using System;
using System.Collections.Generic;


namespace GigHub.Repositories
{
    public interface IAttendanceRepository
    {

        IEnumerable<Attendance> GetFutureAttendances(String userId);
        Attendance GetAttendance(String userId, int gigId);
        
    }
}