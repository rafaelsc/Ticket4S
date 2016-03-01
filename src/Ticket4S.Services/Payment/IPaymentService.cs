using JetBrains.Annotations;
using Ticket4S.Services.Payment.Model;

namespace Ticket4S.Services.Payment
{
    public interface IPaymentService
    {
        [NotNull]
        PaymentResult PayWithCreditCard([NotNull] BillingWithCreditCard billingData); //TODO: Implementar Async
    }
}