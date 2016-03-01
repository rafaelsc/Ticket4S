using System.Threading.Tasks;
using JetBrains.Annotations;
using Ticket4S.Services.Email.Model;

namespace Ticket4S.Services.Email
{
    public interface ISendEmailService
    {
        [NotNull]
        Task SendAsync([NotNull] EMailToSend emailData);
    }
}
