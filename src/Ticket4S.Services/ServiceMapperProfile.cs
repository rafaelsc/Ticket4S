using AutoMapper;
using Ticket4S.Entity.GatewayPayment;
using Ticket4S.Entity.Purchase;
using Ticket4S.Services.Notification.Model;
using Ticket4S.Services.Payment.Model;

namespace Ticket4S.Services
{
    public class ServiceMapperProfile : Profile
    {
        public override string ProfileName => "Payment Services";

        protected override void Configure()
        {
            CreateMap<PurchaseOrder, TransactionHistory>()
                .ForMember(dest => dest.Id, e => e.Ignore())
                .ForMember(dest => dest.TransactionDateTime, e => e.Ignore())
                .ForMember(dest => dest.PaymentBilledSuccessful, e => e.Ignore())
                .ForMember(dest => dest.OrderIdInTheGatewaySystem, e => e.Ignore())
                .ForMember(dest => dest.OperationMessage, e => e.Ignore())
                .ForMember(dest => dest.DebugRawData, e => e.Ignore());

            CreateMap<PurchaseOrder, PurchaseDetails>()
                .ForMember(dest => dest.PurchaseId, e => e.MapFrom(srv => srv.Id));

            CreateMap<PaymentResult, TransactionHistory>()
                .ForMember(dest => dest.Id, e => e.Ignore())
                .ForMember(dest => dest.TransactionDateTime, e => e.Ignore())
                .ForMember(dest => dest.BuyerUserId, e => e.Ignore())
                .ForMember(dest => dest.BuyerUser, e => e.Ignore())
                .ForMember(dest => dest.BoughtEventId, e => e.Ignore())
                .ForMember(dest => dest.BoughtEvent, e => e.Ignore())
                .ForMember(dest => dest.BoughtTicketId, e => e.Ignore())
                .ForMember(dest => dest.BoughtTicket, e => e.Ignore())
                .ForMember(dest => dest.BilledValue, e => e.Ignore())
                .ForMember(dest => dest.UserRequestToSaveCreditCard, e => e.Ignore())
                .ForMember(dest => dest.CreatedAt, e => e.Ignore());

            CreateMap<Ticket4S.Services.Payment.Model.SavedCreditCard, Ticket4S.Entity.GatewayPayment.SavedCreditCard>()
                .ForMember(dest => dest.Id, e=> e.Ignore())
                .ForMember(dest => dest.UserId, e => e.Ignore())
                .ForMember(dest => dest.User, e => e.Ignore())
                .ForMember(dest => dest.CreatedAt, e => e.Ignore());
        }
    }
}
