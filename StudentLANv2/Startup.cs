using Microsoft.Owin;
using Owin;
using BL;

[assembly: OwinStartupAttribute(typeof(StudentLANv2.Startup))]
namespace StudentLANv2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            OwinStartup.ConfigureAuth(app);
        }
    }
}
