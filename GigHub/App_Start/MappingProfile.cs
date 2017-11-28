using AutoMapper;
using GigHub.Controllers.Api;
using GigHub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GigHub.App_Start
{
    public class MappingProfile : Profile {
        public MappingProfile()
        {
            CreateMap<Notification, NotificationDTO>();
            CreateMap<Genre, GenreDTO>();
            CreateMap<ApplicationUser, UserDTO>();
            CreateMap<Gig, GigDTO>(); 
        }
    }
}