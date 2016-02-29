using System;
using System.Reactive.Linq;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Xunit.Abstractions;

namespace Ticket4S.CommonTest
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
                .MinimumLevel.Verbose()
                .WriteTo.Trace()
                .WriteTo.Observers(logEvents => logEvents
                    .Do(le => _output.WriteLine(le.RenderMessage()))
                    .Subscribe())
                .CreateLogger();
        }
    }
}
