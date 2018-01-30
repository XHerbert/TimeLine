using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TimeLine.Startup))]
namespace TimeLine
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
