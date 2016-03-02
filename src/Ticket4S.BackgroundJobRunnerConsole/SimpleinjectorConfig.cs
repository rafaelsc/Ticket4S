using System.Data.Entity;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using Ticket4S.Entity;

namespace Ticket4S.BackgroundJobRunner
{
    public static class SimpleinjectorConfig
    {
        public static Container Configure()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();

            // Register your types, for instance:
            //////////////////////////////////////////////////////////////////////////////

            container.Register<Ticket4SDbContext>(Lifestyle.Scoped);

            //////////////////////////////////////////////////////////////////////////////
            
            container.Verify();

            return container;
        }
    }
}