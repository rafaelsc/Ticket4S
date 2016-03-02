using Microsoft.Owin;
using Owin;
using Ticket4S.Web.App_Start;

[assembly: OwinStartupAttribute(typeof(Ticket4S.Web.Startup))]

namespace Ticket4S.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            SerilogConfig.Configure();

            ConfigureAuth(app);

            var container = SimpleinjectorConfig.Configure(app);
            HangfireConfig.Configure(app, container);
        }
    }
}
