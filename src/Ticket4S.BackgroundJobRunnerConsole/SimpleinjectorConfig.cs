using System.Data.Entity;
using AutoMapper;
using Serilog;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;
using Ticket4S.Entity;
using Ticket4S.MundipaggService;
using Ticket4S.SendGridService;
using Ticket4S.Services.Email;
using Ticket4S.Services.Notification;
using Ticket4S.Services.Payment;
using Ticket4S.Services.Purchase;

namespace Ticket4S.BackgroundJobRunner
{
    public static class SimpleinjectorConfig
    {
        public static Container Configure(MapperConfiguration mapperConfig)
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new ExecutionContextScopeLifestyle();

            // Register your types, for instance:
            //////////////////////////////////////////////////////////////////////////////

            container.Register<Ticket4SDbContext>(Lifestyle.Scoped);

            container.Register<PurchaseService>(Lifestyle.Scoped);

            container.Register<IPaymentService, MundpaggPaymentService>(Lifestyle.Scoped);
            container.Register<IPurchaseAccomplishedNotifyService, PurchaseAccomplishedEmailNotifyService>(Lifestyle.Scoped);
            container.Register<ISendEmailService, SendGridSendEmailService>(Lifestyle.Scoped);

            container.Register<ILogger>(() => Log.Logger, Lifestyle.Scoped);
            //container.RegisterConditional(typeof(ILogger), c => Log.Logger, Lifestyle.Transient, c => true );

            container.RegisterSingleton<MapperConfiguration>(mapperConfig);
            container.RegisterSingleton<IMapper>(() => container.GetInstance<MapperConfiguration>().CreateMapper(container.GetInstance));
            
            //////////////////////////////////////////////////////////////////////////////
#if DEBUG
            container.Verify();
#endif
            return container;
        }
    }
}