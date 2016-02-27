using JetBrains.Annotations;
using Ticket4S.Services.Pagamento.Model;

namespace Ticket4S.Services.Pagamento
{
    public interface IPagamentoService
    {
        [NotNull]
        ResultadoDoPagamento PagarComCartaoDeCredito([NotNull] CobrancaViaCartaoDeCredito dadosDaCobranca);
    }
}