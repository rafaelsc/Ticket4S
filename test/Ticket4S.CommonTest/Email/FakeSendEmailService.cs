using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Serilog;
using Ticket4S.Services.Email;
using Ticket4S.Services.Email.Model;
using Ticket4S.Utils;

namespace Ticket4S.CommonTest.Email
{
    public class FakeSendEmailService : ISendEmailService
    {
        protected ILogger Log { get; }

        public FakeSendEmailService(ILogger log)
        {
            Log = log.ForContext<FakeSendEmailService>();
        }

        public Task SendAsync(EMailToSend emailData)
        {
            Contract.Requires(emailData != null);
            Contract.Ensures(Contract.Result<Task>() != null);
            ValidatorHelper.ThrowesIfHasDataAnnotationError(nameof(emailData), emailData);

            Log.Information("Fake: EmailService.EnviarEmailAsync()");

            return Task.FromResult(0);
        }
    }
}