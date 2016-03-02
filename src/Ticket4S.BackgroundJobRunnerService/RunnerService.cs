using System.ServiceProcess;
using Hangfire;
using Ticket4S.BackgroundJobRunner;

namespace Ticket4S.BackgroundJobRunnerService
{
    public partial class RunnerService : ServiceBase
    {
        private BackgroundJobServer _server;

        public RunnerService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            SerilogConfig.Configure();
            var container = SimpleinjectorConfig.Configure();
            HangfireConfig.Configure(container);

            _server = new BackgroundJobServer();
        }

        protected override void OnStop()
        {
            _server?.Dispose();
        }
    }
}
