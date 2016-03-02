﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using SimpleInjector;
using SimpleInjector.Advanced;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using Ticket4S.Entity;
using Ticket4S.Entity.User;

namespace Ticket4S.Web.App_Start
{
    public static class SimpleinjectorConfig
    {
        public static Container Configure(IAppBuilder app)
        {
            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            container.Options.ConstructorResolutionBehavior = new GreediestConstructorBehavior();

            // Register your types, for instance:
            //////////////////////////////////////////////////////////////////////////////
            //container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped);

            container.RegisterPerWebRequest<Ticket4SDbContext>();
            container.RegisterPerWebRequest<DbContext>(()=> container.GetInstance<Ticket4SDbContext>());
            
            //////////////////////////////////////////////////////////////////////////////
            
            container.RegisterPerWebRequest<IdentityFactoryOptions<ApplicationUserManager>>(() => new IdentityFactoryOptions<ApplicationUserManager>()
                {
                    DataProtectionProvider = app.GetDataProtectionProvider()
                });
            container.RegisterPerWebRequest<IAuthenticationManager>(() => container.IsVerifying() ? new OwinContext().Authentication : HttpContext.Current.GetOwinContext().Authentication);

            container.RegisterPerWebRequest<ApplicationUserManager>();
            container.RegisterPerWebRequest<ApplicationSignInManager>();
            
            container.RegisterPerWebRequest<IUserStore<User>, UserStore<User>>();
            container.RegisterPerWebRequest<UserManager<User>>();
            container.RegisterPerWebRequest<IRoleStore<IdentityRole, string>, RoleStore<IdentityRole, string, IdentityUserRole>>();
            container.RegisterPerWebRequest<RoleManager<IdentityRole>>();
            
            //////////////////////////////////////////////////////////////////////////////

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            // This is an extension method from the integration package as well.
            container.RegisterMvcIntegratedFilterProvider();

            //////////////////////////////////////////////////////////////////////////////

            container.Verify();

            //////////////////////////////////////////////////////////////////////////////
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));

            return container;
        }

        public class GreediestConstructorBehavior : IConstructorResolutionBehavior
        {
            public ConstructorInfo GetConstructor(Type serviceType, Type implementationType)
            {
                if (implementationType.IsInterface)
                    return null;

                var cctor = (from ctor in implementationType.GetConstructors()
                             orderby ctor.GetParameters().Length descending
                             select ctor).FirstOrDefault();

                return cctor;
            }
        }
    }
}
