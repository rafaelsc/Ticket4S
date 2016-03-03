using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Serilog;
using Ticket4S.Entity.User;
using Ticket4S.Services.Email;
using Ticket4S.Services.Email.Model;
using Ticket4S.Services.Notification.Model;

namespace Ticket4S.Services.Notification
{
    public interface IPurchaseAccomplishedNotifyService
    {
        [NotNull]
        Task NotifyPurchaseCompleteSuccessfulAsync([NotNull] User toUser, [NotNull] PurchaseDetails purchaseData);
        [NotNull]
        Task NotifyPurchaseNotCompletedAsync([NotNull] User toUser, [NotNull] PurchaseDetails purchaseData, RejectionReason reason, [CanBeNull] string failMessage);
    }

    public class PurchaseAccomplishedEmailNotifyService : IPurchaseAccomplishedNotifyService
    {
        private ILogger Log { get; }
        private IMapper Mapper { get; }
        private ISendEmailService SendEmailService { get; }

        public PurchaseAccomplishedEmailNotifyService(ILogger log, IMapper mapper, ISendEmailService sendEmailService)
        {
            Log = log.ForContext<PurchaseAccomplishedEmailNotifyService>();
            Mapper = mapper;
            SendEmailService = sendEmailService;
        }


        public async Task NotifyPurchaseCompleteSuccessfulAsync(User toUser, PurchaseDetails purchaseData)
        {
            if (string.IsNullOrEmpty(toUser.Email))
                return;

            var eMail = new EMailToSend()
            {
                DestinationEMail = toUser.Email,
                Subject = @"Parabens, sua Compra no Ticket4S foi realizada com sucesso.",
                Body = $@"
Senhor(a) 

Seu ingresso para o espetaculo {purchaseData.BoughtEvent.Name} foi comprado com sucesso.

Lembre-se de pegar ele na bilheteria {purchaseData.BoughtEvent.EventPlace.Name} 1 hora do show.


Obrigado por comrpar na Ticket4S.
"
            };

            await SendEmailService.SendAsync(eMail);
        }

        public async Task NotifyPurchaseNotCompletedAsync(User toUser, PurchaseDetails purchaseData, RejectionReason reason, string failMessage)
        {
            if (string.IsNullOrEmpty(toUser.Email))
                return;

            var eMail = new EMailToSend()
            {
                DestinationEMail = toUser.Email,
                Subject = @"Desculpe, mas tivemos um problema na sua Compra no Ticket4S.",
                Body = $@"
Senhor(a) 

Houve algum problema na sua compra do ingresso para o espetaculo {purchaseData.BoughtEvent.Name}.

Detalhes do erro: {failMessage}.

Pedimos para tentar comprar novamente, tente outro cartão de credito.

Obrigado por comrpar na Ticket4S.
"
            };

            await SendEmailService.SendAsync(eMail);
        }
    }
}
