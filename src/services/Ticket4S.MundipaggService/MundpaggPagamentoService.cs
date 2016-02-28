using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using GatewayApiClient;
using GatewayApiClient.DataContracts;
using GatewayApiClient.DataContracts.EnumTypes;
using Ticket4S.Services.Pagamento;
using Ticket4S.Services.Pagamento.Model;

namespace Ticket4S.MundipaggService
{
    public class MundpaggPagamentoService : IPagamentoService
    {
        private IMapper Mapper { get; }

        public MundpaggPagamentoService(IMapper mapper)
        {
            Mapper = mapper;
        }

        public ResultadoDoPagamento PagarComCartaoDeCredito(CobrancaViaCartaoDeCredito dadosDaCobranca)
        {
            Contract.Requires(dadosDaCobranca != null);
            Contract.Ensures(Contract.Result<ResultadoDoPagamento>() != null);


            var dadosDaTransacaoDeCartao = Mapper.Map<CreditCardTransaction>(dadosDaCobranca);
            var requestData = new CreateSaleRequest()
            {
                CreditCardTransactionCollection = new Collection<CreditCardTransaction>(new[] { dadosDaTransacaoDeCartao }),
                Order = new Order()
                {
                    OrderReference = dadosDaCobranca.Id.ToString()
                }
            };

            var client = new GatewayServiceClient();
            var response = client.Sale.Create(requestData);

            var pedidoCriado = response.HttpStatusCode == HttpStatusCode.Created;
            var sucesso = pedidoCriado && (response.Response?.CreditCardTransactionResultCollection.All(t => t.Success) ?? false);
            var idDoPedido = response.Response?.OrderResult?.OrderKey.ToString();
            var messagem = ObterMensagem(erroFoiDeValidacaoDeDados: pedidoCriado == false, respostaDoGateway: response.Response); //TODO
            var rawData = response.RawResponse;

            return new ResultadoDoPagamento(sucesso, idDoPedido, messagem, rawData);
        }

        private static string ObterMensagem(bool erroFoiDeValidacaoDeDados, CreateSaleResponse respostaDoGateway)
        {
            return erroFoiDeValidacaoDeDados ? ObterMensagemDeErro(respostaDoGateway) : ObterMensagemDaTransacaoFinanceira(respostaDoGateway);
        }

        private static string ObterMensagemDeErro(CreateSaleResponse respostaDoGateway)
        {
            var str = from errorItem in respostaDoGateway.ErrorReport.ErrorItemCollection
                      select errorItem.Description;

            return string.Join(", ", str);
        }

        private static string ObterMensagemDaTransacaoFinanceira(CreateSaleResponse respostaDoGateway)
        {
            var str = from transacao in respostaDoGateway.CreditCardTransactionResultCollection
                      select transacao.AcquirerMessage;
            return string.Join(", ", str);
        }
    }
}
