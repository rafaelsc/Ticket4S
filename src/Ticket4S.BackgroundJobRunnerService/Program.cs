using System;
using System.IO;
using System.ServiceProcess;

namespace Ticket4S.BackgroundJobRunnerService
{
    static class Program
    {
        static void Main()
        {
#if DEBUG
            //Altearnao o caminho fisico do DataDirecory para a mesma base LocalBD do Projeto Web
            var webDataDirPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Ticket4S.Web\App_Data\"));
            AppDomain.CurrentDomain.SetData("DataDirectory", webDataDirPath);
#endif

            var servicesToRun = new ServiceBase[] { new RunnerService() };
            ServiceBase.Run(servicesToRun);
        }
    }
}
