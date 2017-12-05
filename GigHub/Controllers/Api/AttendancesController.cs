using GigHub.Core.Dto;
using GigHub.Core.Models;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();
            var exists = _context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId);

            if (exists)
                return BadRequest("Attendance already exists.");

            
            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok() ;
        }

        [HttpDelete]
        public IHttpActionResult DeleteAttende(int id)
        {
            var userId = User.Identity.GetUserId();
            var attende = _context.Attendances.SingleOrDefault(a => a.AttendeeId == userId && a.GigId == id);

            if (attende == null)
                return NotFound();

            _context.Attendances.Remove(attende);
            _context.SaveChanges();

            return Ok(id);

        }
    }
}
