using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using FluentAssertions;
using GatewayApiClient;
using GatewayApiClient.DataContracts;
using GatewayApiClient.DataContracts.EnumTypes;
using Xunit;

namespace Ticket4S.MundipaggServiceTest
{
    public class DirectPaymentGatewayTest
    {
        [Fact]
        public void CobrancaNoCartaoComResultadoAprovado()
        {
            // Arrange
            var transaction = new CreditCardTransaction()
            {
                AmountInCents = 100,
                CreditCard = new CreditCard()
                {
                    CreditCardNumber = "4111111111111111",
                    CreditCardBrand = CreditCardBrandEnum.Visa,
                    ExpMonth = 10,
                    ExpYear = 2018,
                    SecurityCode = "123",
                    HolderName = "Smith"
                }
            };

            // Act
            var client = new GatewayServiceClient();
            var response = client.Sale.Create(transaction);

            // Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.Created);
            response.Response?.OrderResult?.OrderKey.Should().NotBeEmpty();
            response.Response?.CreditCardTransactionResultCollection.Should().NotBeNullOrEmpty();
            response.Response?.CreditCardTransactionResultCollection.All(t => t.Success).Should().BeTrue();
        }

        [Fact]
        public void CobrancaNoCartaoComResultadoNaoAtorizado()
        {
            // Arrange
            var transaction = new CreditCardTransaction()
            {
                AmountInCents = 1002121115485484,
                CreditCard = new CreditCard()
                {
                    CreditCardNumber = "4111111111111111",
                    CreditCardBrand = CreditCardBrandEnum.Visa,
                    ExpMonth = 10,
                    ExpYear = 2018,
                    SecurityCode = "123",
                    HolderName = "Smith"
                }
            };

            // Act
            var client = new GatewayServiceClient();
            var response = client.Sale.Create(transaction);

            // Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.Created);
            response.Response?.OrderResult?.OrderKey.Should().NotBeEmpty();
            response.Response?.CreditCardTransactionResultCollection.Should().NotBeNullOrEmpty();
            response.Response?.CreditCardTransactionResultCollection.All(t => t.Success).Should().BeFalse();
        }

        [Fact]
        public void CobrancaNoCartaoComFalhaDeComunicacao()
        {
            // Arrange
            var transaction = new CreditCardTransaction()
            {
                AmountInCents = 105001,
                CreditCard = new CreditCard()
                {
                    CreditCardNumber = "4111111111111111",
                    CreditCardBrand = CreditCardBrandEnum.Visa,
                    ExpMonth = 10,
                    ExpYear = 2018,
                    SecurityCode = "123",
                    HolderName = "Smith"
                }
            };

            // Act
            var client = new GatewayServiceClient();
            var response = client.Sale.Create(transaction);

            // Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.Created);
            response.Response?.OrderResult?.OrderKey.Should().NotBeEmpty();
            response.Response?.CreditCardTransactionResultCollection.Should().NotBeNullOrEmpty();
            response.Response?.CreditCardTransactionResultCollection.All(t => t.Success).Should().BeFalse();
        }

        [Fact]
        public void CobrancaNoCartaoFaltandoDados1()
        {
            // Arrange
            var transaction = new CreditCardTransaction()
            {
                AmountInCents = 0,
                CreditCard = new CreditCard()
                {
                    CreditCardNumber = "4111111111111111",
                    CreditCardBrand = CreditCardBrandEnum.Visa,
                    ExpMonth = 10,
                    ExpYear = 2018,
                    SecurityCode = "123",
                    HolderName = "Smith"
                }
            };

            // Act
            var client = new GatewayServiceClient();
            var response = client.Sale.Create(transaction);

            // Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.Response?.OrderResult.Should().BeNull();
            response.Response?.CreditCardTransactionResultCollection.Should().BeNullOrEmpty();

            response.Response?.ErrorReport.Should().NotBeNull();
            response.Response?.ErrorReport.ErrorItemCollection.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void CobrancaNoCartaoFaltandoDados2()
        {
            // Arrange
            var transaction = new CreditCardTransaction()
            {
                AmountInCents = 100,
                CreditCard = new CreditCard()
                {
                    CreditCardNumber = "4111111111111111",
                    CreditCardBrand = CreditCardBrandEnum.Visa,
                    ExpMonth = 10,
                    ExpYear = 1900,
                    SecurityCode = "123",
                    HolderName = "Smith"
                }
            };

            // Act
            var client = new GatewayServiceClient();
            var response = client.Sale.Create(transaction);

            // Assert
            response.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.Response?.OrderResult.Should().BeNull();
            response.Response?.CreditCardTransactionResultCollection.Should().BeNullOrEmpty();

            response.Response?.ErrorReport.Should().NotBeNull();
            response.Response?.ErrorReport.ErrorItemCollection.Should().NotBeNullOrEmpty();
        }
    }
}
