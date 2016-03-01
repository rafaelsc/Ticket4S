using System;
using System.Diagnostics.Contracts;
using Serilog;
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

        public PaymentResult PayWithCreditCard(BillingWithCreditCard billingData)
        {
            Contract.Requires(billingData != null);
            Contract.Ensures(Contract.Result<PaymentResult>() != null);
            ValidatorHelper.ThrowesIfHasDataAnnotationErro(nameof(billingData), billingData);

            Log.Information("Fake: PagamentoService.PagarComCartaoDeCredito() = true");

            return new PaymentResult(true, Guid.NewGuid().ToString(), "FAKE - SIMULADO - Sumulacao Interna do Sistema", "");
        }
    }
}