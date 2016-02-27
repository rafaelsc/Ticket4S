using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ticket4S.Web.Startup))]

namespace Ticket4S.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
