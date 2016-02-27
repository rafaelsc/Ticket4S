namespace Ticket4S.Services.Pagamento.Model
{
    public class ResultadoDoPagamento
    {
        public ResultadoDoPagamento(bool sucesso, string idDoPedido, string messagem, string rawData)
        {
            Sucesso = sucesso;
            IdDoPedido = idDoPedido;
            Messagem = messagem;
            RawData = rawData;
        }

        public bool Sucesso { get; }

        public string IdDoPedido { get; }

        public string Messagem { get; }

        public string RawData { get; }
    }
}