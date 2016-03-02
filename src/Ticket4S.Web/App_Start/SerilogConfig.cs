using Serilog;
using Serilog.Events;
using Serilog.Exceptions;

namespace Ticket4S.Web
{
    public static class SerilogConfig
    {
        public static ILogger Configure()
        {
            Log.Logger = CreateBaseConfiguration()
                            .CreateLogger();

            return Log.Logger;
        }
        
        private static LoggerConfiguration CreateBaseConfiguration()
        {
            return new LoggerConfiguration()
                .Enrich.WithExceptionDetails()
                .Enrich.WithThreadId()
                .MinimumLevel.Debug()
                .WriteTo.Trace()
                .WriteTo.RollingFile(@"Log\Site-{Date}.log")
                .WriteTo.RollingFile(@"Log\Error-{Date}.log", LogEventLevel.Debug);
        }
    }
}