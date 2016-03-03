using System;
using System.Diagnostics.Contracts;
using Serilog;
using Ticket4S.Entity;
using Ticket4S.Services.Payment;
using Ticket4S.Services.Payment.Model;
using Ticket4S.Utils;

namespace Ticket4S.CommonTest.Pagamento
{
    public class SuceessFakePaymentService : IPaymentService
    {
        protected ILogger Log { get; }

        public SuceessFakePaymentService(ILogger log)
        {
            Log = log.ForContext<SuceessFakePaymentService>();
        }

        public PaymentResult PayWithCreditCard(BillingWithCreditCardBase billingData)
        {
            Contract.Requires(billingData != null);
            Contract.Ensures(Contract.Result<PaymentResult>() != null);
            ValidatorHelper.ThrowesIfHasDataAnnotationError(nameof(billingData), billingData);

            Log.Information("Fake: PaymentService.PayWithCreditCard(BillingWithCreditCardBase) = true");

            return new PaymentResult(true, Guid.NewGuid().ToString(), "FAKE - SIMULADO - Sumulacao Interna do Sistema", "", new SavedCreditCard(Guid.NewGuid().ToString(), CreditCardBrand.AndarAki, "****1111"));
        }
    }
}