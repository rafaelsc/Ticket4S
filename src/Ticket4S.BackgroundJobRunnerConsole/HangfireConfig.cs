using Hangfire;
using Hangfire.SimpleInjector;
using SimpleInjector;

namespace Ticket4S.BackgroundJobRunner
{
    public static class HangfireConfig
    {
        public static void Configure(Container simpleInjectorContainer)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");
            GlobalConfiguration.Configuration.UseSerilogLogProvider();
            GlobalConfiguration.Configuration.UseActivator(new SimpleInjectorJobActivator(simpleInjectorContainer));
        }
    }
}