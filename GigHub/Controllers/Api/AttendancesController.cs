using GigHub.Core.Dto;
using GigHub.Core.Models;
using GigHub.Persistence;
using GigHub.Repositories;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;


        public AttendancesController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();
            var exists = _unitOfWork.Attendances.GetAttendance(userId, dto.GigId) != null;
              

            if (exists)
                return BadRequest("Attendance already exists.");

            
            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            _unitOfWork.Attendances.Add(attendance);
            _unitOfWork.Complete();

            return Ok() ;
        }

        [HttpDelete]
        public IHttpActionResult DeleteAttende(int id)
        {
            var userId = User.Identity.GetUserId();
            var attende = _unitOfWork.Attendances.GetAttendance(userId, id);
           

            if (attende == null)
                return NotFound();

            _unitOfWork.Attendances.Remove(attende);
            _unitOfWork.Complete();

            return Ok(id);

        }
    }
}
