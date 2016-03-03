using System;
using AutoMapper;
using GatewayApiClient.DataContracts;
using Ticket4S.Extensions;
using Ticket4S.Services.Payment.Model;
using BillingAddress = Ticket4S.Services.Payment.Model.BillingAddress;

namespace Ticket4S.MundipaggService
{
    public class MundpaggGatewayMapperProfile : Profile
    {
        public override string ProfileName => "Gateway Payment Mundpagg";

        protected override void Configure()
        {
            CreateMap<CreditCardInfo, CreditCard>()
                //.ForMember(dest => dest.CreditCardNumber, exp => exp.MapFrom(src => src.CreditCardNumber.ToUnsecureString()))
                //.ForMember(dest => dest.SecurityCode, exp => exp.MapFrom(src => src.SecurityCode.ToUnsecureString()))
                .ForMember(dest => dest.InstantBuyKey, exp => exp.Ignore())
                .ForMember(dest => dest.BillingAddress, exp => exp.Ignore());

            CreateMap<BillingAddress, GatewayApiClient.DataContracts.BillingAddress>();

            CreateMap<BillingWithNewCreditCard, CreditCardTransaction>()
                .ForMember(dest => dest.AmountInCents, exp => exp.MapFrom(src => (long) (src.Value*100)))
                .ForMember(dest => dest.Options, exp => exp.Ignore())
                .ForMember(dest => dest.Recurrency, exp => exp.Ignore())
                .ForMember(dest => dest.InstallmentCount, exp => exp.Ignore())
                .ForMember(dest => dest.CreditCardOperation, exp => exp.Ignore())
                .ForMember(dest => dest.TransactionReference, exp => exp.Ignore())
                .ForMember(dest => dest.TransactionDateInMerchant, exp => exp.Ignore());
            //.ForMember(dest => dest.CreditCard.BillingAddress, exp => exp.MapFrom(src => src.BillingAddress)); //TODO: Caso seja Requerido.

            CreateMap<BillingWithSavedCreditCard, CreditCardTransaction>()
                .ForMember(dest => dest.CreditCard, exp => exp.MapFrom(src => new CreditCard() {InstantBuyKey = Guid.Parse(src.IdOfSavedCardInTheGateway)}))
                .ForMember(dest => dest.AmountInCents, exp => exp.MapFrom(src => (long)(src.Value * 100)))
                .ForMember(dest => dest.Options, exp => exp.Ignore())
                .ForMember(dest => dest.Recurrency, exp => exp.Ignore())
                .ForMember(dest => dest.InstallmentCount, exp => exp.Ignore())
                .ForMember(dest => dest.CreditCardOperation, exp => exp.Ignore())
                .ForMember(dest => dest.TransactionReference, exp => exp.Ignore())
                .ForMember(dest => dest.TransactionDateInMerchant, exp => exp.Ignore());
        }
    }
}
