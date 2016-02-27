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

            var sucesso = response.HttpStatusCode == HttpStatusCode.Created && VerificarTransacao(response.Response);
            var idDoPedido = response.Response?.OrderResult?.OrderKey.ToString();
            var messagem = ""; //TODO
            var rawData = response.RawResponse;


            return new ResultadoDoPagamento(sucesso, idDoPedido, messagem, rawData);

            var createSaleResponse = response.Response;
            if (response.HttpStatusCode == HttpStatusCode.Created)
            {
                foreach (var creditCardTransaction in createSaleResponse.CreditCardTransactionResultCollection)
                {
                    Debug.WriteLine(creditCardTransaction.AcquirerMessage);
                }
            }
            else {
                if (createSaleResponse.ErrorReport != null)
                {
                    foreach (var errorItem in createSaleResponse.ErrorReport.ErrorItemCollection)
                    {
                        Debug.WriteLine(@"$Error {0}: {1}", errorItem.ErrorCode, errorItem.Description);
                    }
                }
            }
        }

        private static bool VerificarTransacao(CreateSaleResponse response)
        {
            if (response == null)
                return false;

            //var todosOsResultacomComSucesso = response.CreditCardTransactionResultCollection.Any(t => t.CreditCardTransactionStatus != CreditCardTransactionStatusEnum.Captured);
            var todosOsResultacomComSucesso = response.CreditCardTransactionResultCollection.All(t => t.Success);
            return todosOsResultacomComSucesso;
        }
    }
}
