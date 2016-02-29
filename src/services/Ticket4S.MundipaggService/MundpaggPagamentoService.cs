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
using Serilog;
using Ticket4S.Services.Pagamento;
using Ticket4S.Services.Pagamento.Model;
using Ticket4S.Utils;

namespace Ticket4S.MundipaggService
{
    public class MundpaggPagamentoService : IPagamentoService
    {
        private ILogger Log { get; }
        private IMapper Mapper { get; }

        public MundpaggPagamentoService(IMapper mapper, ILogger log)
        {
            Log = log.ForContext<MundpaggPagamentoService>();
            Mapper = mapper;
        }

        public ResultadoDoPagamento PagarComCartaoDeCredito(CobrancaViaCartaoDeCredito dadosDaCobranca)
        {
            Contract.Requires(dadosDaCobranca != null);
            Contract.Ensures(Contract.Result<ResultadoDoPagamento>() != null);
            ValidatorHelper.ThrowesIfHasDataAnnotationErro(nameof(dadosDaCobranca), dadosDaCobranca);

            Log.Information("Iniciada cobrança no Cartao de Credito");

            var dadosDaTransacaoDeCartao = Mapper.Map<CreditCardTransaction>(dadosDaCobranca);
            var requestData = new CreateSaleRequest()
            {
                CreditCardTransactionCollection = new Collection<CreditCardTransaction>(new[] { dadosDaTransacaoDeCartao }),
                Order = new Order()
                {
                    OrderReference = dadosDaCobranca.Id.ToString()
                }
            };

            Log.Debug("Dados Cartao de Credito. {dadosDaCobranca}", dadosDaCobranca);

            try
            {
                Log.Debug("Criando Client do Gateway de Pagamentos");
                var client = new GatewayServiceClient();

                Log.Debug("Enviando dados de Cobrança para o Gateway");
                var response = client.Sale.Create(requestData);
                Log.Debug("Enviando dados de Cobrança para o Gateway - Finaliazado");


                var pedidoCriado = response.HttpStatusCode == HttpStatusCode.Created;
                var sucesso = pedidoCriado && (response.Response?.CreditCardTransactionResultCollection.All(t => t.Success) ?? false);
                var idDoPedido = response.Response?.OrderResult?.OrderKey.ToString();
                var messagem = ObterMensagem(erroFoiDeValidacaoDeDados: pedidoCriado == false, respostaDoGateway: response.Response); //TODO
                var rawData = response.RawResponse;

                var resultado = new ResultadoDoPagamento(sucesso, idDoPedido, messagem, rawData);
                Log.Debug("Resultado da Cobranca, {ResultadoDoPagamento}", resultado);

                if (resultado.PagamentoCobradoComSucesso)
                {
                    Log.Information("Cobrança no Cartao de Credito realizada com Sucesso.");
                }
                else
                {
                    Log.Error("Erro durante a cobrança de cartão de credito. Cobrança não realizada. {mensagem}", messagem);
                }

                return resultado;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro GRAVE INTERNO durante a cobrança de cartão de credito. Cobrança não realizada. {Exception}", ex);
                return new ResultadoDoPagamento(false, null, "ERRO Interno do Sistema", ex.Message);
            }
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
