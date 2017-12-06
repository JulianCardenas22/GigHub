using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.test.Extensions;
using GigHub.Core.Repositories;
using System.Web.Http.Results;
using FluentAssertions;
using GigHub.Core.Models;

namespace GigHub.test.Controllers.API
{
    [TestClass]
    public class GigsControllersTests
    {
        private GigsController _controller;
        private Mock<IGigRepository> _mockRepository;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _userId = "1";
            _mockRepository = new Mock<IGigRepository>();
            var mockUnitOfWork =    new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(u => u.Gigs).Returns(_mockRepository.Object);
                
            _controller = new GigsController(mockUnitOfWork.Object);
            _controller.MockCurrentUser(_userId, "user1@domain.com");

        }

        [TestMethod]
        public void Cancel_NoGigWihGivenIdExists_ShouldReturnNotFound()
        {
          var result = _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_GigIsCanceled_ShouldReturnNotFound()
        {
            var gig = new Gig();
            gig.Cancel();

            _mockRepository.Setup(r => r.GetGigWithAttendee(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_UserCancelingAnotherUserGig_ShouldReturnUnauthorized()
        {
            var gig = new Gig(){
                ArtistId = _userId + " - "
            };

            _mockRepository.Setup(r => r.GetGigWithAttendee(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<UnauthorizedResult>();

        }

        [TestMethod]
        public void Cancel_ValidRequest_ShouldReturnOK()
        {
            var gig = new Gig { ArtistId = _userId };

            _mockRepository.Setup(r => r.GetGigWithAttendee(1)).Returns(gig);

            var result = _controller.Cancel(1);

            result.Should().BeOfType<OkResult>();

        }
    }
}
