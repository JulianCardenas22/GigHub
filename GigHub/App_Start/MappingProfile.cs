using AutoMapper;

using GigHub.Dto;
using GigHub.Models;


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