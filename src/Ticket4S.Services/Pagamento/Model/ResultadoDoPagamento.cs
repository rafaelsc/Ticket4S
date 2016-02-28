namespace Ticket4S.Services.Pagamento.Model
{
    public class ResultadoDoPagamento
    {
        public ResultadoDoPagamento(bool pagamentoCobradoComSucesso, string idDoPedidoNoSistemaDePagamento, string messagemDeRespostaDaOperacao, string debugRawData)
        {
            PagamentoCobradoComSucesso = pagamentoCobradoComSucesso;
            IdDoPedidoNoSistemaDePagamento = idDoPedidoNoSistemaDePagamento;
            MessagemDeRespostaDaOperacao = messagemDeRespostaDaOperacao;
            DebugRawData = debugRawData;
        }

        public bool PagamentoCobradoComSucesso { get; }

        public string IdDoPedidoNoSistemaDePagamento { get; }

        public string MessagemDeRespostaDaOperacao { get; }

        public string DebugRawData { get; }
    }
}