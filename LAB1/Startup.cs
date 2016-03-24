using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LAB1.Startup))]
namespace LAB1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
