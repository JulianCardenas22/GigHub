
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GigHub.Core.Repositories;
using GigHub.Core;
using GigHub.Controllers.Api;
using GigHub.test.Extensions;
using GigHub.Core.Models;
using GigHub.Core.Dto;
using FluentAssertions;
using System.Web.Http.Results;

namespace GigHub.test.Controllers.API
{
    [TestClass]
    public class AttendancesControllersTests
    {
        private AttendancesController _controller;
        private string _userId;
        private Mock<IAttendanceRepository> _mockRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _userId = "1";
            _mockRepository = new Mock<IAttendanceRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(u => u.Attendances).Returns(_mockRepository.Object);
            _controller = new AttendancesController(mockUnitOfWork.Object);
            _controller.MockCurrentUser(_userId, "user1@domain.com");

        }

        [TestMethod]
        public void Attend_UserAttendingAGigForWhichHeHasAnAttendance_ShouldReturnBadRequest()
        {
            var attendance = new Attendance();
            _mockRepository.Setup(r => r.GetAttendance(_userId,1)).Returns(attendance);

            var result = _controller.Attend(new AttendanceDto() {GigId=1 });

            result.Should().BeOfType<BadRequestResult>();
        }

        [TestMethod]
        public void Attend_ValidRequest_ShouldReturnOk()
        {
            var result = _controller.Attend(new AttendanceDto { GigId = 1 });

            result.Should().BeOfType<OkResult>();
        }


        [TestMethod]
        public void Delete_AttendancesNull_ShouldReturnNotFound()
        {
            var result = _controller.DeleteAttende(1);

            result.Should().BeOfType<NotFoundResult>();
        }



        [TestMethod]
        public void Delete_ValidRequest_ShouldReturnOk()
        {
            var attendance = new Attendance();
            _mockRepository.Setup(a => a.GetAttendance(_userId, 1)).Returns(attendance);

            var result = _controller.DeleteAttende(1);

            result.Should().BeOfType<OkNegotiatedContentResult<int>>();
        }


        [TestMethod]
        public void Delete_ValidRequest_ReturnIdValue_ShouldReturnOk()
        {
            var attendance = new Attendance();
            _mockRepository.Setup(a => a.GetAttendance(_userId, 1)).Returns(attendance);

            var result = (OkNegotiatedContentResult<int>) _controller.DeleteAttende(1);

            result.Content.Should().Be(1);
        }
    }
}
