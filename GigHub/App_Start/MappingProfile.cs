using AutoMapper;
using GigHub.Core.Dto;
using GigHub.Core.Models;


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