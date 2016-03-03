using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using Serilog;
using SimpleInjector;
using SimpleInjector.Advanced;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using Ticket4S.Entity;
using Ticket4S.Entity.User;
using Ticket4S.MundipaggService;
using Ticket4S.SendGridService;
using Ticket4S.Services.Email;
using Ticket4S.Services.Notification;
using Ticket4S.Services.Payment;
using Ticket4S.Services.Purchase;

namespace Ticket4S.Web.App_Start
{
    public static class SimpleinjectorConfig
    {
        public static Container Configure(IAppBuilder app, MapperConfiguration mapperConfig)
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

            container.RegisterSingleton<MapperConfiguration>(mapperConfig);
            container.RegisterSingleton<IMapper>(() => container.GetInstance<MapperConfiguration>().CreateMapper(container.GetInstance));

            //////////////////////////////////////////////////////////////////////////////

            container.Register<ILogger>(() => Log.Logger, Lifestyle.Scoped);

            container.Register<PurchaseService>(Lifestyle.Scoped);
            container.Register<IPaymentService, MundpaggPaymentService>(Lifestyle.Scoped);
            container.Register<IPurchaseAccomplishedNotifyService, PurchaseAccomplishedEmailNotifyService>(Lifestyle.Scoped);
            container.Register<ISendEmailService, SendGridSendEmailService>(Lifestyle.Scoped);

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
#if DEBUG
            container.Verify();
#endif
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
