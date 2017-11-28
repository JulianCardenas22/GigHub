using AutoMapper;
using GigHub.Models;
using Microsoft.Owin;
using Owin;
using GigHub.Controllers.Api;

[assembly: OwinStartupAttribute(typeof(GigHub.Startup))]
namespace GigHub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            Mapper.Initialize(mapper => {
                mapper.CreateMap<Notification, NotificationDTO>();
                mapper.CreateMap<Genre, GenreDTO>();
                mapper.CreateMap<ApplicationUser, UserDTO>();
                mapper.CreateMap<Gig, GigDTO>();
            });
        }
    }
}
