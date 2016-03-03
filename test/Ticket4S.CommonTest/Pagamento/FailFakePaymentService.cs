using System;
using System.Diagnostics.Contracts;
using Serilog;
using Ticket4S.Services.Payment;
using Ticket4S.Services.Payment.Model;
using Ticket4S.Utils;

namespace Ticket4S.CommonTest.Pagamento
{
    public class FailFakePaymentService : IPaymentService
    {
        protected ILogger Log { get; }

        public FailFakePaymentService(ILogger log)
        {
            Log = log.ForContext<FailFakePaymentService>();
        }

        public PaymentResult PayWithCreditCard(BillingWithCreditCardBase billingData)
        {
            Contract.Requires(billingData != null);
            Contract.Ensures(Contract.Result<PaymentResult>() != null);
            ValidatorHelper.ThrowesIfHasDataAnnotationError(nameof(billingData), billingData);

            Log.Information("Fake: PaymentService.PayWithCreditCard(BillingWithCreditCardBase) => false");

            return new PaymentResult(false, Guid.NewGuid().ToString(), "FAKE - SIMULADO - Sumulacao Interna do Sistema", "", null);
        }
    }
}