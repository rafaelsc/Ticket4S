using Hangfire;
using Hangfire.SimpleInjector;
using JetBrains.Annotations;
using Owin;
using SimpleInjector;

namespace Ticket4S.Web
{
    public static class HangfireConfig
    {
        public static void Configure([NotNull] IAppBuilder app, [NotNull] Container simpleInjectorContainer)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");
            GlobalConfiguration.Configuration.UseSerilogLogProvider();
            GlobalConfiguration.Configuration.UseActivator(new SimpleInjectorJobActivator(simpleInjectorContainer));

            //TODO: Configure Auth - http://docs.hangfire.io/en/latest/configuration/using-dashboard.html
            app.UseHangfireDashboard("/jobs");
        }
    }
}