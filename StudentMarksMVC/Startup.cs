using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentMarksMVC.Startup))]
namespace StudentMarksMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
