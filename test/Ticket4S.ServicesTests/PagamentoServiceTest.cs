﻿using System;
using AutoMapper;
using FluentAssertions;
using Ticket4S.CommonTest;
using Ticket4S.Entity;
using Ticket4S.Extensions;
using Ticket4S.MundipaggService;
using Ticket4S.Services.Payment;
using Ticket4S.Services.Payment.Model;
using Xunit;
using Xunit.Abstractions;

namespace Ticket4S.ServicesTests
{
    public class PagamentoServiceTest: BaseTest
    {
        private readonly IMapper _mapper;
        
        public PagamentoServiceTest(ITestOutputHelper output) : base(output)
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<MundpaggGatewayMapperProfile>());
            mapperConfig.AssertConfigurationIsValid();

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public void CobrancaNoCartaoComResultadoAprovado()
        {
            // Arrange
            var billingData = new BillingWithNewCreditCard()
            {
                Id = Guid.NewGuid(),
                CreditCard =  new CreditCardInfo()
                {
                    CreditCardBrand = CreditCardBrand.Mastercard,
                    HolderName = "Astrogildo Silcva",
                    CreditCardNumber = "4111111111111111",//.ToSecureString(),
                    SecurityCode = "321",//.ToSecureString(),
                    ExpMonth = 06,
                    ExpYear = 2020,
                },
                Value = 100.00M
            };

            // Act
            IPaymentService paymentService = new MundpaggPaymentService(_mapper, Log);
            var result = paymentService.PayWithCreditCard(billingData);

            // Assert
            result.Should().NotBeNull();
            result.DebugRawData.Should().NotBeNullOrWhiteSpace();
            result.PaymentBilledSuccessful.Should().BeTrue();
            result.OrderIdInTheGatewaySystem.Should().NotBeNullOrWhiteSpace();
            result.OperationMessage.Should().NotBeNullOrWhiteSpace();
            result.SavedCreditCard.Should().NotBeNull();
        }

        [Fact]
        public void CobrancaNoCartaoComResultadoNaoAtorizado()
        {
            // Arrange
            var billingData = new BillingWithNewCreditCard()
            {
                Id = Guid.NewGuid(),
                CreditCard = new CreditCardInfo()
                {
                    CreditCardBrand = CreditCardBrand.Mastercard,
                    HolderName = "Astrogildo Silcva",
                    CreditCardNumber = "4111111111111111",//.ToSecureString(),
                    SecurityCode = "321",//.ToSecureString(),
                    ExpMonth = 06,
                    ExpYear = 2020,
                },
                Value = 10021.84M
            };

            // Act
            IPaymentService paymentService = new MundpaggPaymentService(_mapper, Log);
            var result = paymentService.PayWithCreditCard(billingData);

            // Assert
            result.Should().NotBeNull();
            result.DebugRawData.Should().NotBeNullOrWhiteSpace();
            result.PaymentBilledSuccessful.Should().BeFalse();
            result.OrderIdInTheGatewaySystem.Should().NotBeNullOrWhiteSpace();
            result.OperationMessage.Should().NotBeNullOrWhiteSpace();
            result.SavedCreditCard.Should().NotBeNull();
        }

        [Fact]
        public void CobrancaNoCartaoComFalhaDeComunicacao()
        {
            // Arrange
            var billingData = new BillingWithNewCreditCard()
            {
                Id = Guid.NewGuid(),
                CreditCard = new CreditCardInfo()
                {
                    CreditCardBrand = CreditCardBrand.Mastercard,
                    HolderName = "Astrogildo Silcva",
                    CreditCardNumber = "4111111111111111",//.ToSecureString(),
                    SecurityCode = "321",//.ToSecureString(),
                    ExpMonth = 06,
                    ExpYear = 2020,
                },
                Value = 1050.01M
            };

            // Act
            IPaymentService paymentService = new MundpaggPaymentService(_mapper, Log);
            var result = paymentService.PayWithCreditCard(billingData);

            // Assert
            result.Should().NotBeNull();
            result.DebugRawData.Should().NotBeNullOrWhiteSpace();
            result.PaymentBilledSuccessful.Should().BeFalse();
            result.OrderIdInTheGatewaySystem.Should().NotBeNullOrWhiteSpace();
            result.OperationMessage.Should().NotBeNullOrWhiteSpace();
            result.SavedCreditCard.Should().NotBeNull();
        }

        [Fact]
        public void CobrancaNoCartaoFaltandoSemPassarValor()
        {
            // Arrange
            var billingData = new BillingWithNewCreditCard()
            {
                Id = Guid.NewGuid(),
                CreditCard = new CreditCardInfo()
                {
                    CreditCardBrand = CreditCardBrand.Mastercard,
                    HolderName = "Astrogildo Silcva",
                    CreditCardNumber = "4111111111111111",//.ToSecureString(),
                    SecurityCode = "321",//.ToSecureString(),
                    ExpMonth = 06,
                    ExpYear = 2020,
                },
                //Valor = 0.10M
            };

            // Act
            IPaymentService paymentService = new MundpaggPaymentService(_mapper, Log);

            // Assert
            Assert.Throws<ArgumentException>(() => paymentService.PayWithCreditCard(billingData));
        }

        [Fact]
        public void CobrancaNoCartaoFaltandoDados1()
        {
            // Arrange
            var billingData = new BillingWithNewCreditCard()
            {
                Id = Guid.NewGuid(),
                CreditCard = new CreditCardInfo()
                {
                    CreditCardBrand = CreditCardBrand.Mastercard,
                    HolderName = "Astrogildo Silva",
                    CreditCardNumber = "",//.ToSecureString(),
                    SecurityCode = "321",//.ToSecureString(),
                    ExpMonth = 06,
                    ExpYear = 2020,
                },
                Value = 0.10M
            };

            // Act
            IPaymentService paymentService = new MundpaggPaymentService(_mapper, Log);
            var result = paymentService.PayWithCreditCard(billingData);

            // Assert
            result.Should().NotBeNull();
            result.DebugRawData.Should().NotBeNullOrWhiteSpace();
            result.PaymentBilledSuccessful.Should().BeFalse();
            result.OrderIdInTheGatewaySystem.Should().BeNull();
            result.OperationMessage.Should().NotBeNullOrWhiteSpace();
            result.OperationMessage.Should().Be("O número do cartão deve ter no mínimo 10 dígitos e no máximo 24 digitos.");
            result.SavedCreditCard.Should().BeNull();
        }

        [Fact]
        public void CobrancaNoCartaoFaltandoDados2()
        {
            // Arrange
            var billingData = new BillingWithNewCreditCard()
            {
                Id = Guid.NewGuid(),
                CreditCard = new CreditCardInfo()
                {
                    CreditCardBrand = CreditCardBrand.Mastercard,
                    HolderName = "Astrogildo Silcva",
                    CreditCardNumber = "4111111111111111",//.ToSecureString(),
                    SecurityCode = "321",//.ToSecureString(),
                    ExpMonth = 04,
                    ExpYear = 1500,
                },
                Value = 1000.00M
            };

            // Act
            IPaymentService paymentService = new MundpaggPaymentService(_mapper, Log);
            var result = paymentService.PayWithCreditCard(billingData);

            // Assert
            result.Should().NotBeNull();
            result.DebugRawData.Should().NotBeNullOrWhiteSpace();
            result.PaymentBilledSuccessful.Should().BeFalse();
            result.OrderIdInTheGatewaySystem.Should().BeNull();
            result.OperationMessage.Should().NotBeNullOrWhiteSpace();
            result.OperationMessage.Should().Be("Data de vencimento do cartão expirada.");
            result.SavedCreditCard.Should().BeNull();
        }

        ////////////////////////

        [Fact]
        public void CobrancaNoCartaoSalvoComResultadoAprovado()
        {
            // Arrange
            var billingData = new BillingWithSavedCreditCard()
            {
                Id = Guid.NewGuid(),
                IdOfSavedCardInTheGateway = "f5bd4d18-a7d9-4176-9b98-b9f82a5a9e87",
                Value = 100.00M
            };

            // Act
            IPaymentService paymentService = new MundpaggPaymentService(_mapper, Log);
            var result = paymentService.PayWithCreditCard(billingData);

            // Assert
            result.Should().NotBeNull();
            result.DebugRawData.Should().NotBeNullOrWhiteSpace();
            result.PaymentBilledSuccessful.Should().BeTrue();
            result.OrderIdInTheGatewaySystem.Should().NotBeNullOrWhiteSpace();
            result.OperationMessage.Should().NotBeNullOrWhiteSpace();
            result.SavedCreditCard.Should().NotBeNull();
        }

        [Fact]
        public void CobrancaNoCartaoSalvoComResultadoNaoAtorizado()
        {
            // Arrange
            var billingData = new BillingWithSavedCreditCard()
            {
                Id = Guid.NewGuid(),
                IdOfSavedCardInTheGateway = "f5bd4d18-a7d9-4176-9b98-b9f82a5a9e87",
                Value = 10021.84M
            };

            // Act
            IPaymentService paymentService = new MundpaggPaymentService(_mapper, Log);
            var result = paymentService.PayWithCreditCard(billingData);

            // Assert
            result.Should().NotBeNull();
            result.DebugRawData.Should().NotBeNullOrWhiteSpace();
            result.PaymentBilledSuccessful.Should().BeFalse();
            result.OrderIdInTheGatewaySystem.Should().NotBeNullOrWhiteSpace();
            result.OperationMessage.Should().NotBeNullOrWhiteSpace();
            result.SavedCreditCard.Should().NotBeNull();
        }

        [Fact]
        public void CobrancaNoCartaoSalvoComFalhaDeComunicacao()
        {
            // Arrange
            var billingData = new BillingWithSavedCreditCard()
            {
                Id = Guid.NewGuid(),
                IdOfSavedCardInTheGateway = "f5bd4d18-a7d9-4176-9b98-b9f82a5a9e87",
                Value = 1050.01M
            };

            // Act
            IPaymentService paymentService = new MundpaggPaymentService(_mapper, Log);
            var result = paymentService.PayWithCreditCard(billingData);

            // Assert
            result.Should().NotBeNull();
            result.DebugRawData.Should().NotBeNullOrWhiteSpace();
            result.PaymentBilledSuccessful.Should().BeFalse();
            result.OrderIdInTheGatewaySystem.Should().NotBeNullOrWhiteSpace();
            result.OperationMessage.Should().NotBeNullOrWhiteSpace();
            result.SavedCreditCard.Should().NotBeNull();
        }

        [Fact]
        public void CobrancaNoCartaoSalvoFaltandoSemPassarValor()
        {
            // Arrange
            var billingData = new BillingWithSavedCreditCard()
            {
                Id = Guid.NewGuid(),
                IdOfSavedCardInTheGateway = "f5bd4d18-a7d9-4176-9b98-b9f82a5a9e87",
                //Valor = 0.10M
            };

            // Act
            IPaymentService paymentService = new MundpaggPaymentService(_mapper, Log);

            // Assert
            Assert.Throws<ArgumentException>(() => paymentService.PayWithCreditCard(billingData));
        }
    }
}
