using System;
using System.Linq;
using AutoMapper;
using Ticket4S.MundipaggService;
using Ticket4S.Services.Pagamento;
using Xunit;

namespace Ticket4S.ServicesTests
{
    public class PagamentoServiceTest
    {
        private readonly IMapper _mapper;
        
        public PagamentoServiceTest()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperConfiguration>());
            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public void CobrancaNoCartaoComResultadoAprovado()
        {
            // Arrange


            // Act
            IPagamentoService pagamentoService = new MundpaggPagamentoService(_mapper);
            //pagamentoService.PagarComCartaoDeCredito(cobrancaData);

            // Assert
        }

        [Fact]
        public void CobrancaNoCartaoComResultadoNaoAtorizado()
        {
            // Arrange
           
            // Act
            
            // Assert
            
        }

        [Fact]
        public void CobrancaNoCartaoComFalhaDeComunicacao()
        {
            // Arrange
            

            // Act
            

            // Assert
          
        }

        [Fact]
        public void CobrancaNoCartaoFaltandoDados1()
        {
            // Arrange
        

            // Act
           
            // Assert
         
        }

        [Fact]
        public void CobrancaNoCartaoFaltandoDados2()
        {
            // Arrange
            

            // Act
           
            // Assert
           
        }
    }
}
