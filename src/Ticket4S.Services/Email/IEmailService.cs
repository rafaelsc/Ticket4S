using System.Threading.Tasks;
using JetBrains.Annotations;
using Ticket4S.Services.Email.Model;

namespace Ticket4S.Services.Email
{
    public interface IEmailService
    {
        [NotNull]
        Task EnviarEmailAsync([NotNull] EMailAEnviar dadosDoEmail);
    }
}
