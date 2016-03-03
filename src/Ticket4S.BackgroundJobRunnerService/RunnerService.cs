using System.ServiceProcess;
using Hangfire;
using Ticket4S.BackgroundJobRunner;
using Ticket4S.Web;
using HangfireConfig = Ticket4S.BackgroundJobRunner.HangfireConfig;
using SerilogConfig = Ticket4S.BackgroundJobRunner.SerilogConfig;

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
            var mapper = AutoMapperConfig.Config();
            var container = SimpleinjectorConfig.Configure(mapper);
            HangfireConfig.Configure(container);

            _server = new BackgroundJobServer();
        }

        protected override void OnStop()
        {
            _server?.Dispose();
        }
    }
}
