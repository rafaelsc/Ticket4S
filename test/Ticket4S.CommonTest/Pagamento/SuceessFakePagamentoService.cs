using System;
using System.Diagnostics.Contracts;
using Serilog;
using Ticket4S.Services.Pagamento;
using Ticket4S.Services.Pagamento.Model;
using Ticket4S.Utils;

namespace Ticket4S.CommonTest.Pagamento
{
    public class SuceessFakePagamentoService : IPagamentoService
    {
        protected ILogger Log { get; }

        public SuceessFakePagamentoService(ILogger log)
        {
            Log = log.ForContext<SuceessFakePagamentoService>();
        }

        public ResultadoDoPagamento PagarComCartaoDeCredito(CobrancaViaCartaoDeCredito dadosDaCobranca)
        {
            Contract.Requires(dadosDaCobranca != null);
            Contract.Ensures(Contract.Result<ResultadoDoPagamento>() != null);
            ValidatorHelper.ThrowesIfHasDataAnnotationErro(nameof(dadosDaCobranca), dadosDaCobranca);

            Log.Information("Fake: PagamentoService.PagarComCartaoDeCredito() = true");

            return new ResultadoDoPagamento(true, Guid.NewGuid().ToString(), "FAKE - SIMULADO - Sumulacao Interna do Sistema", "");
        }
    }
}