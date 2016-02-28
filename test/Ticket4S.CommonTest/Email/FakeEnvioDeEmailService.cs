using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using Serilog;
using Ticket4S.Services.Email;
using Ticket4S.Services.Email.Model;
using Ticket4S.Utils;

namespace Ticket4S.CommonTest.Email
{
    public class FakeEnvioDeEmailService : IEnvioDeEmailService
    {
        protected ILogger Log { get; }

        public FakeEnvioDeEmailService(ILogger log)
        {
            Log = log.ForContext<FakeEnvioDeEmailService>();
        }

        public Task EnviarAsync(EMailAEnviar dadosDoEmail)
        {
            Contract.Requires(dadosDoEmail != null);
            Contract.Ensures(Contract.Result<Task>() != null);
            ValidatorHelper.ThrowesIfHasDataAnnotationErro(nameof(dadosDoEmail), dadosDoEmail);

            Log.Information("Fake: EmailService.EnviarEmailAsync()");

            return Task.FromResult(0);
        }
    }
}