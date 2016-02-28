using System;
using System.Reactive.Linq;
using Serilog;
using Serilog.Exceptions;
using Xunit.Abstractions;

namespace Ticket4S.ServicesTests
{
    public abstract class BaseTest
    {
        private readonly ITestOutputHelper _output;
        protected ILogger Log { get; }

        protected BaseTest(ITestOutputHelper output)
        {
            _output = output;

            Log = new LoggerConfiguration()
                .Enrich.WithExceptionDetails()
                .MinimumLevel.Debug()
                .WriteTo.Trace()
                .WriteTo.Observers(logEvents => logEvents
                    .Do(le => _output.WriteLine(le.RenderMessage()))
                    .Subscribe())
                .CreateLogger();
        }
    }
}
