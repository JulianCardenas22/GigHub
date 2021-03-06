﻿using AutoMapper;
using GigHub.Core.Dto;
using GigHub.Core.Models;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class NotificationsController : ApiController
    {
        private ApplicationDbContext _context;

        public NotificationsController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<NotificationDTO> GetNewNotifications()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications.Where(un => un.UserId == userId && !un.IsRead)
                                                          .Select(n => n.Notification)
                                                          .Include(n => n.Gig.Artist)
                                                          .ToList();
           
            return notifications.Select(Mapper.Map<Notification,NotificationDTO>);
        }

        [HttpPost]
        public IHttpActionResult MarkAsRead()
        {
            var userId = User.Identity.GetUserId();
            var notifications = _context.UserNotifications.Where(n => n.UserId == userId && !n.IsRead).ToList();

            notifications.ForEach(n => n.Read());
            _context.SaveChanges();
            return Ok();
        }
    }
}
