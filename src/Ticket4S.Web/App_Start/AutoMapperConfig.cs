using AutoMapper;
using Ticket4S.MundipaggService;
using Ticket4S.Services;
using Ticket4S.Web.AutoMapper;

namespace Ticket4S.Web
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration Config()
        {
            var mapperConfig = new MapperConfiguration(cfg => 
            {
                cfg.AddProfile<WebSiteMapperProfile>();
                cfg.AddProfile<ServiceMapperProfile>();
                cfg.AddProfile<MundpaggGatewayMapperProfile>();
            });

#if DEBUG
            mapperConfig.AssertConfigurationIsValid();
#endif

            return mapperConfig;
        }
    }
}
