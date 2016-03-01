using System;
using System.Diagnostics.Contracts;
using Serilog;
using Ticket4S.Services.Pagamento;
using Ticket4S.Services.Pagamento.Model;
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

        public PaymentResult PayWithCreditCard(BillingWithCreditCard billingData)
        {
            Contract.Requires(billingData != null);
            Contract.Ensures(Contract.Result<PaymentResult>() != null);
            ValidatorHelper.ThrowesIfHasDataAnnotationErro(nameof(billingData), billingData);

            Log.Information("Fake: PagamentoService.PagarComCartaoDeCredito() => false");

            return new PaymentResult(false, Guid.NewGuid().ToString(), "FAKE - SIMULADO - Sumulacao Interna do Sistema", "");
        }
    }
}