using AutoMapper;
using Ticket4S.Web.AutoMapper;

namespace Ticket4S.Web
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration Config()
        {
            var mapperConfig = new MapperConfiguration(cfg => 
                cfg.AddProfile<WebSiteMapperProfile>()
            );

#if DEBUG
            mapperConfig.AssertConfigurationIsValid();
#endif

            return mapperConfig;
        }
    }
}
