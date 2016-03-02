using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Ticket4S.BackgroundJobRunnerService
{
    static class Program
    {
        static void Main()
        {
            var servicesToRun = new ServiceBase[] { new RunnerService() };
            ServiceBase.Run(servicesToRun);
        }
    }
}
