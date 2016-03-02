using System;
using System.IO;
using Hangfire;
using Ticket4S.BackgroundJobRunner;

namespace Ticket4S.BackgroundJobRunnerConsole
{
    static class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            //Altearnao o caminho fisico do DataDirecory para a mesma base LocalBD do Projeto Web
            var webDataDirPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\Ticket4S.Web\App_Data\"));
            AppDomain.CurrentDomain.SetData("DataDirectory", webDataDirPath);
#endif

            SerilogConfig.ConfigureWithConsole();
            var container = SimpleinjectorConfig.Configure();
            HangfireConfig.Configure(container);

            using (var server = new BackgroundJobServer())
            {
                Console.WriteLine("Hangfire Server started. Press ENTER key to exit...");
                Console.ReadLine();
            }
        }
    }
}
