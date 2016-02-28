using AutoMapper;
using GatewayApiClient.DataContracts;
using Ticket4S.Extensions;
using Ticket4S.Services.Pagamento.Model;

namespace Ticket4S.MundipaggService
{
    public class AutoMapperConfiguration : Profile
    {
        public override string ProfileName => "Gateway Pagamento Mundpagg";

        protected override void Configure()
        {
            CreateMap<CartaoDeCredito, CreditCard>()
                .ForMember(dest => dest.CreditCardNumber, exp => exp.MapFrom(src => src.Numero.ToUnsecureString()))
                .ForMember(dest => dest.SecurityCode, exp => exp.MapFrom(src => src.CodigoDeSeguranca.ToUnsecureString()))
                .ForMember(dest => dest.CreditCardBrand, exp => exp.MapFrom(src => src.Bandeira))
                .ForMember(dest => dest.HolderName, exp => exp.MapFrom(src => src.NomeDoDono))
                .ForMember(dest => dest.ExpYear, exp => exp.MapFrom(src => src.ExpiracaoAno))
                .ForMember(dest => dest.ExpMonth, exp => exp.MapFrom(src => src.ExpiracaoMes))
                .ForMember(dest => dest.InstantBuyKey, exp => exp.Ignore())
                .ForMember(dest => dest.BillingAddress, exp => exp.Ignore());

            CreateMap<EnderecoDeCobranca, BillingAddress>()
                .ForMember(dest => dest.Country, exp => exp.MapFrom(src => src.Pais))
                .ForMember(dest => dest.State, exp => exp.MapFrom(src => src.UF))
                .ForMember(dest => dest.City, exp => exp.MapFrom(src => src.Cidade))
                .ForMember(dest => dest.District, exp => exp.MapFrom(src => src.Bairro))
                .ForMember(dest => dest.Street, exp => exp.MapFrom(src => src.Rua))
                .ForMember(dest => dest.Number, exp => exp.MapFrom(src => src.Numero))
                .ForMember(dest => dest.Complement, exp => exp.MapFrom(src => src.Complemento))
                .ForMember(dest => dest.ZipCode, exp => exp.MapFrom(src => src.CEP));

            CreateMap<CobrancaViaCartaoDeCredito, CreditCardTransaction>()
                .ForMember(dest => dest.AmountInCents, exp => exp.MapFrom(src => (long) (src.Valor*100)))
                .ForMember(dest => dest.CreditCard, exp => exp.MapFrom(src => src.CartaoDeCredito))
                .ForMember(dest => dest.Options, exp => exp.Ignore())
                .ForMember(dest => dest.Recurrency, exp => exp.Ignore())
                .ForMember(dest => dest.InstallmentCount, exp => exp.Ignore())
                .ForMember(dest => dest.CreditCardOperation, exp => exp.Ignore())
                .ForMember(dest => dest.TransactionReference, exp => exp.Ignore())
                .ForMember(dest => dest.TransactionDateInMerchant, exp => exp.Ignore());
                //.ForMember(dest => dest.CreditCard.BillingAddress, exp => exp.MapFrom(src => src.EnderecoDeCobranca));
        }
    }
}
