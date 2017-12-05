using AutoMapper;
using Microsoft.Owin;
using Owin;
using GigHub.App_Start;

[assembly: OwinStartupAttribute(typeof(GigHub.Startup))]
namespace GigHub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            Mapper.Initialize(c => c.AddProfile<MappingProfile>());

        }
    }
}
