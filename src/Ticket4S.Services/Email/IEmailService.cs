using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Ticket4S.Services.Email.Model;
using Ticket4S.Utils;

namespace Ticket4S.Services.Email
{
    public interface IEmailService
    {
        [NotNull]
        Task EnviarEmailAsync([NotNull] EMailAEnviar dadosDoEmail);
    }

    public class NullEmailService : IEmailService
    {
        public Task EnviarEmailAsync(EMailAEnviar dadosDoEmail)
        {
            Contract.Requires(dadosDoEmail != null);
            Contract.Ensures(Contract.Result<Task>() != null);
            ValidatorHelper.ThrowesIfNotValid(nameof(dadosDoEmail), dadosDoEmail);

            return Task.FromResult(0);
        }
    }
}
