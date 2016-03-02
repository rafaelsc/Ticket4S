using System;
using Hangfire;
using Ticket4S.BackgroundJobRunner;

namespace Ticket4S.BackgroundJobRunnerConsole
{
    static class Program
    {
        static void Main(string[] args)
        {
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
