using System;
using System.Diagnostics.Contracts;
using System.Threading.Tasks;
using AutoMapper;
using JetBrains.Annotations;
using Serilog;
using Ticket4S.Entity;
using Ticket4S.Entity.GatewayPayment;
using Ticket4S.Entity.Purchase;
using Ticket4S.Services.Notification;
using Ticket4S.Services.Notification.Model;
using Ticket4S.Services.Payment;
using Ticket4S.Services.Payment.Model;
using Ticket4S.Utils;
using SavedCreditCard = Ticket4S.Entity.GatewayPayment.SavedCreditCard;

namespace Ticket4S.Services.Purchase
{
    public class PurchaseService
    {
        private ILogger Log { get; }
        private IMapper Mapper { get; }
        private IPurchaseAccomplishedNotifyService NotifyService { get; }
        private IPaymentService PaymentService { get; }
        private Ticket4SDbContext Db { get; }

        public PurchaseService(ILogger log, IMapper mapper, IPurchaseAccomplishedNotifyService notifyService, IPaymentService paymentService, Ticket4SDbContext db)
        {
            Log = log.ForContext<PurchaseService>();
            Mapper = mapper;
            NotifyService = notifyService;
            PaymentService = paymentService;
            Db = db;
        }

        public async Task PurchaseTicketAsync(Guid purchaseOrderId, BillingWithCreditCardBase billingData)
        {
            Contract.Requires(billingData != null);
            ValidatorHelper.ThrowesIfHasDataAnnotationError(nameof(billingData), billingData);

            Log.Information("Inicio do Processo do Pedido de Compra agendada em Plano de Fundo. {purchaseOrderId}", purchaseOrderId);
            var order = await Db.PurchaseOrders.FindAsync(purchaseOrderId);
            if(order.Condition != Condition.WaitingProcessing)
            {
                Log.Information("Ordem Já processada. Pulando. {Order}", order);
                return;
            }
            
            await PurchaseTicketInternalAsync(order, billingData);
        }

        private async Task PurchaseTicketInternalAsync([NotNull] PurchaseOrder order, BillingWithCreditCardBase billingData)
        {
            Log.Information("Processo de Compra de Ingresso - Iniciado. {Date}", DateTimeOffset.Now);

            var transactionHistory = new TransactionHistory()
            {
                Id = Guid.NewGuid(),
                TransactionDateTime = DateTimeOffset.Now,
            };
            Mapper.Map(order, transactionHistory);

            var result = PaymentService.PayWithCreditCard(billingData);
            Log.Information("Retonado do Gateway de Pagamento. {Date} - {Result}", DateTimeOffset.Now, result);

            Mapper.Map(result, transactionHistory);
            transactionHistory.CreatedAt = DateTimeOffset.Now;

            Db.TransactionHistory.Add(transactionHistory);
            await Db.SaveChangesAsync();

            if (result.PaymentBilledSuccessful)
            {
                await PaymentOkAsync(order, result); ;
            }
            else
            {
                await PaymentErrorAsync(order, result);
            }

            Log.Information("Processo de Compra de Ingresso - Finalizado. {Date}", DateTimeOffset.Now);
        }

        private async Task PaymentOkAsync(PurchaseOrder order, PaymentResult result)
        {
            order.Condition = Condition.ProccessedSuccessful;
            order.ChangedAt = DateTimeOffset.Now;
            await Db.SaveChangesAsync();

            if (order.UserRequestToSaveCreditCard)
            {
                SavedCreditCard scc = Mapper.Map<SavedCreditCard>(result.SavedCreditCard);
                scc.Id = Guid.NewGuid();
                scc.UserId = order.BuyerUserId;
                scc.CreatedAt = DateTimeOffset.Now;
                Db.SavedCreditCards.Add(scc);
                await Db.SaveChangesAsync();
            }

            //Todo Agendar pro Hangfire
            await NotifyService.NotifyPurchaseCompleteSuccessfulAsync(order.BuyerUser, Mapper.Map<PurchaseDetails>(order));
        }

        private async Task PaymentErrorAsync(PurchaseOrder order, PaymentResult result)
        {
            order.Condition = Condition.ProccessedFail;
            order.ChangedAt = DateTimeOffset.Now;

            await Db.SaveChangesAsync();

            //Todo Agendar pro Hangfire
            await NotifyService.NotifyPurchaseNotCompletedAsync(order.BuyerUser, 
                Mapper.Map<PurchaseDetails>(order), 
                result.OrderCreatedAtGateway ? RejectionReason.PaymentDeclined : RejectionReason.ErrorDuringPayment, 
                result.OperationMessage);
        }
    }
}
