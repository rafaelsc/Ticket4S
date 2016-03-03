using AutoMapper;
using Ticket4S.Entity.GatewayPayment;
using Ticket4S.Entity.Purchase;
using Ticket4S.Services.Notification.Model;
using Ticket4S.Services.Payment.Model;

namespace Ticket4S.MundipaggService.AutoMapper
{
    public class ServiceMapperProfile : Profile
    {
        public override string ProfileName => "Payment Services";

        protected override void Configure()
        {
            CreateMap<PurchaseOrder, TransactionHistory>();

            CreateMap<PurchaseOrder, PurchaseDetails>();

            CreateMap<PaymentResult, TransactionHistory>();

            CreateMap<Ticket4S.Services.Payment.Model.SavedCreditCard, Ticket4S.Entity.GatewayPayment.SavedCreditCard>();
        }
    }
}
