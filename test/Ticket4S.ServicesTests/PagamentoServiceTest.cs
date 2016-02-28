using System;
using System.Linq;
using System.Security;
using AutoMapper;
using FluentAssertions;
using Ticket4S.Extensions;
using Ticket4S.MundipaggService;
using Ticket4S.Services.Pagamento;
using Ticket4S.Services.Pagamento.Model;
using Xunit;

namespace Ticket4S.ServicesTests
{
    public class PagamentoServiceTest
    {
        private readonly IMapper _mapper;
        
        public PagamentoServiceTest()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperConfiguration>());
            mapperConfig.AssertConfigurationIsValid();

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public void CobrancaNoCartaoComResultadoAprovado()
        {
            // Arrange
            var cobrancaData = new CobrancaViaCartaoDeCredito()
            {
                Id = Guid.NewGuid(),
                CartaoDeCredito =  new CartaoDeCredito()
                {
                    Bandeira = Bandeira.Mastercard,
                    NomeDoDono = "Astrogildo Silcva",
                    Numero = "4111111111111111".ToSecureString(),
                    CodigoDeSeguranca = "321".ToSecureString(),
                    ExpiracaoMes = 06,
                    ExpiracaoAno = 2020,
                },
                Valor = 100.00M
            };

            // Act
            IPagamentoService pagamentoService = new MundpaggPagamentoService(_mapper);
            var result = pagamentoService.PagarComCartaoDeCredito(cobrancaData);

            // Assert
            result.Should().NotBeNull();
            result.DebugRawData.Should().NotBeNullOrWhiteSpace();
            result.PagamentoCobradoComSucesso.Should().BeTrue();
            result.IdDoPedidoNoSistemaDePagamento.Should().NotBeNullOrWhiteSpace();
            result.MessagemDeRespostaDaOperacao.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void CobrancaNoCartaoComResultadoNaoAtorizado()
        {
            // Arrange
            var cobrancaData = new CobrancaViaCartaoDeCredito()
            {
                Id = Guid.NewGuid(),
                CartaoDeCredito = new CartaoDeCredito()
                {
                    Bandeira = Bandeira.Mastercard,
                    NomeDoDono = "Astrogildo Silcva",
                    Numero = "4111111111111111".ToSecureString(),
                    CodigoDeSeguranca = "321".ToSecureString(),
                    ExpiracaoMes = 06,
                    ExpiracaoAno = 2020,
                },
                Valor = 10021211154854.84M
            };

            // Act
            IPagamentoService pagamentoService = new MundpaggPagamentoService(_mapper);
            var result = pagamentoService.PagarComCartaoDeCredito(cobrancaData);

            // Assert
            result.Should().NotBeNull();
            result.DebugRawData.Should().NotBeNullOrWhiteSpace();
            result.PagamentoCobradoComSucesso.Should().BeFalse();
            result.IdDoPedidoNoSistemaDePagamento.Should().NotBeNullOrWhiteSpace();
            result.MessagemDeRespostaDaOperacao.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void CobrancaNoCartaoComFalhaDeComunicacao()
        {
            // Arrange
            var cobrancaData = new CobrancaViaCartaoDeCredito()
            {
                Id = Guid.NewGuid(),
                CartaoDeCredito = new CartaoDeCredito()
                {
                    Bandeira = Bandeira.Mastercard,
                    NomeDoDono = "Astrogildo Silcva",
                    Numero = "4111111111111111".ToSecureString(),
                    CodigoDeSeguranca = "321".ToSecureString(),
                    ExpiracaoMes = 06,
                    ExpiracaoAno = 2020,
                },
                Valor = 1050.01M
            };

            // Act
            IPagamentoService pagamentoService = new MundpaggPagamentoService(_mapper);
            var result = pagamentoService.PagarComCartaoDeCredito(cobrancaData);

            // Assert
            result.Should().NotBeNull();
            result.DebugRawData.Should().NotBeNullOrWhiteSpace();
            result.PagamentoCobradoComSucesso.Should().BeFalse();
            result.IdDoPedidoNoSistemaDePagamento.Should().NotBeNullOrWhiteSpace();
            result.MessagemDeRespostaDaOperacao.Should().NotBeNullOrWhiteSpace();
        }

        [Fact]
        public void CobrancaNoCartaoFaltandoDados1()
        {
            // Arrange
            var cobrancaData = new CobrancaViaCartaoDeCredito()
            {
                Id = Guid.NewGuid(),
                CartaoDeCredito = new CartaoDeCredito()
                {
                    Bandeira = Bandeira.Mastercard,
                    NomeDoDono = "Astrogildo Silcva",
                    Numero = "4111111111111111".ToSecureString(),
                    CodigoDeSeguranca = "321".ToSecureString(),
                    ExpiracaoMes = 06,
                    ExpiracaoAno = 2020,
                },
                Valor = 0M
            };

            // Act
            IPagamentoService pagamentoService = new MundpaggPagamentoService(_mapper);
            var result = pagamentoService.PagarComCartaoDeCredito(cobrancaData);

            // Assert
            result.Should().NotBeNull();
            result.DebugRawData.Should().NotBeNullOrWhiteSpace();
            result.PagamentoCobradoComSucesso.Should().BeFalse();
            result.IdDoPedidoNoSistemaDePagamento.Should().BeNull();
            result.MessagemDeRespostaDaOperacao.Should().NotBeNullOrWhiteSpace();
            result.MessagemDeRespostaDaOperacao.Should().Be("O valor da transação deve ser maior que zero.");
        }

        [Fact]
        public void CobrancaNoCartaoFaltandoDados2()
        {
            // Arrange
            var cobrancaData = new CobrancaViaCartaoDeCredito()
            {
                Id = Guid.NewGuid(),
                CartaoDeCredito = new CartaoDeCredito()
                {
                    Bandeira = Bandeira.Mastercard,
                    NomeDoDono = "Astrogildo Silcva",
                    Numero = "4111111111111111".ToSecureString(),
                    CodigoDeSeguranca = "321".ToSecureString(),
                    ExpiracaoMes = 04,
                    ExpiracaoAno = 1500,
                },
                Valor = 1000.00M
            };

            // Act
            IPagamentoService pagamentoService = new MundpaggPagamentoService(_mapper);
            var result = pagamentoService.PagarComCartaoDeCredito(cobrancaData);

            // Assert
            result.Should().NotBeNull();
            result.DebugRawData.Should().NotBeNullOrWhiteSpace();
            result.PagamentoCobradoComSucesso.Should().BeFalse();
            result.IdDoPedidoNoSistemaDePagamento.Should().BeNull();
            result.MessagemDeRespostaDaOperacao.Should().NotBeNullOrWhiteSpace();
            result.MessagemDeRespostaDaOperacao.Should().Be("Data de vencimento do cartão expirada.");
        }
    }
}
