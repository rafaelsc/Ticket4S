using System;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Net;
using AutoMapper;
using GatewayApiClient;
using GatewayApiClient.DataContracts;
using Serilog;
using Ticket4S.Entity;
using Ticket4S.Services.Payment;
using Ticket4S.Services.Payment.Model;
using Ticket4S.Utils;
using BillingAddress = GatewayApiClient.DataContracts.BillingAddress;

namespace Ticket4S.MundipaggService
{
    public class MundpaggPaymentService : IPaymentService
    {
        private ILogger Log { get; }
        private IMapper Mapper { get; }

        public MundpaggPaymentService(IMapper mapper, ILogger log)
        {
            Log = log.ForContext<MundpaggPaymentService>();
            Mapper = mapper;
        }

        public PaymentResult PayWithCreditCard(BillingWithCreditCardBase billingData)
        {
            Contract.Requires(billingData != null);
            Contract.Ensures(Contract.Result<PaymentResult>() != null);
            ValidatorHelper.ThrowesIfHasDataAnnotationError(nameof(billingData), billingData);

            Log.Information("Iniciada cobrança no Cartao de Credito");

            var mundpaggCreditCardTransaction = Mapper.Map<CreditCardTransaction>(billingData);
            mundpaggCreditCardTransaction.CreditCard.BillingAddress = Mapper.Map<BillingAddress>((billingData as BillingWithNewCreditCard)?.BillingAddress);

            var requestData = new CreateSaleRequest()
            {
                CreditCardTransactionCollection = new Collection<CreditCardTransaction>(new[] { mundpaggCreditCardTransaction }),
                Order = new Order()
                {
                    OrderReference = billingData.Id.ToString()
                }
            };

            Log.Debug("Dados Cartao de Credito. {dadosDaCobranca}", billingData);

            try
            {
                Log.Debug("Criando Client do Gateway de Pagamentos");
                var client = new GatewayServiceClient();

                Log.Debug("Enviando dados de Cobrança para o Gateway");
                var response = client.Sale.Create(requestData);
                Log.Debug("Enviando dados de Cobrança para o Gateway - Finaliazado");

                var orderGenerated = response.HttpStatusCode == HttpStatusCode.Created;
                var succeful = orderGenerated && (response.Response?.CreditCardTransactionResultCollection.All(t => t.Success) ?? false);
                var orderId = response.Response?.OrderResult?.OrderKey.ToString();
                var message = GetMessage(errorWasValidation: orderGenerated == false, gatewayResponse: response.Response); //TODO
                var rawData = response.RawResponse;
                var savedCc = response.Response?.CreditCardTransactionResultCollection.SingleOrDefault()?.CreditCard;


                var savedCreditCard = savedCc == null ? null : new SavedCreditCard(savedCc.InstantBuyKey.ToString(), Mapper.Map<CreditCardBrand>(savedCc.CreditCardBrand), savedCc.MaskedCreditCardNumber);
                var result = new PaymentResult(succeful, orderId, message, rawData, savedCreditCard);

                Log.Debug("Resultado da Cobranca, {ResultadoDoPagamento}", result);

                if (result.PaymentBilledSuccessful)
                {
                    Log.Information("Cobrança no Cartao de Credito realizada com Sucesso.");
                }
                else
                {
                    Log.Error("Erro durante a cobrança de cartão de credito. Cobrança não realizada. {mensagem}", message);
                }

                return result;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Erro GRAVE INTERNO durante a cobrança de cartão de credito. Cobrança não realizada. {Exception}", ex);
                return new PaymentResult(false, null, "ERRO Interno do Sistema", ex.Message, null);
            }
        }

        private static string GetMessage(bool errorWasValidation, CreateSaleResponse gatewayResponse)
        {
            return errorWasValidation ? GetErrorMessage(gatewayResponse) : GetMessageFromAcquirer(gatewayResponse);
        }

        private static string GetErrorMessage(CreateSaleResponse gatewayResponse)
        {
            var str = from errorItem in gatewayResponse.ErrorReport.ErrorItemCollection
                      select errorItem.Description;

            return string.Join(", ", str);
        }

        private static string GetMessageFromAcquirer(CreateSaleResponse gatewayResponse)
        {
            var str = from transacao in gatewayResponse.CreditCardTransactionResultCollection
                      select transacao.AcquirerMessage;

            return string.Join(", ", str);
        }
    }
}
