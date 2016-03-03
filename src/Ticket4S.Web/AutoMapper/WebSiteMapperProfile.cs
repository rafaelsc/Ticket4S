using AutoMapper;
using Ticket4S.Entity.Event;
using Ticket4S.Entity.Purchase;
using Ticket4S.Extensions;
using Ticket4S.Services.Payment.Model;
using Ticket4S.Web.Controllers;
using Ticket4S.Web.ViewModels;

namespace Ticket4S.Web.AutoMapper
{
    public class WebSiteMapperProfile : Profile
    {
        public override string ProfileName => "Web Site Ticket4S";

        protected override void Configure()
        {
            CreateMap<Event, EventViewModel>();
                //.ForMember(dest => dest.TicketsTypes, exp => exp.MapFrom(src => src.TicketsTypes.OrderBy(t=>t.ViewOrder)));
            
            CreateMap<EventTicketType, EventTicketTypeViewModel>();
            CreateMap<EventTicketType, EventTicketTypeWithEventViewModel>()
                .ForMember(dest=> dest.EventPlace, e=> e.MapFrom(srv=> srv.Event.EventPlace));

            CreateMap<EventPlace, EventPlaceViewModel>();

            CreateMap<OrderViewModel, CreditCardInfo>();
            //.ForMember(dest => dest.CreditCardNumber, e=>e.MapFrom(src=>src.CreditCardNumber.ToSecureString()))
            //.ForMember(dest => dest.SecurityCode, e => e.MapFrom(src => src.SecurityCode.ToSecureString()));

        }
    }
}
