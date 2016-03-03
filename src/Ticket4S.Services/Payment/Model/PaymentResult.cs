using System;
using System.ComponentModel.DataAnnotations;
using Ticket4S.Entity;

namespace Ticket4S.Services.Payment.Model
{
    public class PaymentResult
    {
        public PaymentResult(bool paymentBilledSuccessful, string orderIdInTheGatewaySystem, string operationMessage, string debugRawData, SavedCreditCard savedCreditCard)
        {
            PaymentBilledSuccessful = paymentBilledSuccessful;
            OrderIdInTheGatewaySystem = orderIdInTheGatewaySystem;
            OperationMessage = operationMessage;
            DebugRawData = debugRawData;
            SavedCreditCard = savedCreditCard;
        }

        public bool PaymentBilledSuccessful { get; }
        
        public string OrderIdInTheGatewaySystem { get; }

        public string OperationMessage { get; }

        public string DebugRawData { get; }

        public SavedCreditCard SavedCreditCard { get; }

        public bool OrderCreatedAtGateway => !string.IsNullOrEmpty(OrderIdInTheGatewaySystem);

        public override string ToString() => $"PaymentBilledSuccessful: {PaymentBilledSuccessful}, OrderIdInTheGatewaySystem: {OrderIdInTheGatewaySystem}, OperationMessage: {OperationMessage}";
    }

    public class SavedCreditCard
    {
        public SavedCreditCard(string idOfSavedCardInTheGateway, CreditCardBrand creditCardBrand, string maskedCreditCardNumber)
        {
            IdOfSavedCardInTheGateway = idOfSavedCardInTheGateway;
            CreditCardBrand = creditCardBrand;
            MaskedCreditCardNumber = maskedCreditCardNumber;
        }

        public virtual string IdOfSavedCardInTheGateway { get; }

        public CreditCardBrand CreditCardBrand { get; }

        public string MaskedCreditCardNumber { get; }
    }
}