using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using Ticket4S.Entity;

namespace Ticket4S.Web.App_Start
{
    public static class SimpleinjectorConfig
    {
        public static void Configure()
        {
            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // Register your types, for instance:
            //////////////////////////////////////////////////////////////////////////////
            //container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped);
            
            container.Register<Ticket4SDbContext>();
            container.Register<ApplicationUserManager>();
            container.Register<ApplicationSignInManager>();

            //////////////////////////////////////////////////////////////////////////////

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            // This is an extension method from the integration package as well.
            container.RegisterMvcIntegratedFilterProvider();

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

    }
}
