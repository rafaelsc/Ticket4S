using JetBrains.Annotations;
using Ticket4S.Services.Pagamento.Model;

namespace Ticket4S.Services.Pagamento
{
    public interface IPaymentService
    {
        [NotNull]
        PaymentResult PayWithCreditCard([NotNull] BillingWithCreditCard billingData); //TODO: Implementar Async
    }
}