namespace Ticket4S.Services.Payment.Model
{
    public class PaymentResult
    {
        public PaymentResult(bool paymentBilledSuccessful, string orderIdInTheGatewaySystem, string operationMessage, string debugRawData)
        {
            PaymentBilledSuccessful = paymentBilledSuccessful;
            OrderIdInTheGatewaySystem = orderIdInTheGatewaySystem;
            OperationMessage = operationMessage;
            DebugRawData = debugRawData;
        }

        public bool PaymentBilledSuccessful { get; }
        
        public string OrderIdInTheGatewaySystem { get; }

        public string OperationMessage { get; }

        public string DebugRawData { get; }


        public override string ToString() => $"PaymentBilledSuccessful: {PaymentBilledSuccessful}, OrderIdInTheGatewaySystem: {OrderIdInTheGatewaySystem}, OperationMessage: {OperationMessage}";
    }
}