using System;
using System.ComponentModel.DataAnnotations;
using System.Security;
using JetBrains.Annotations;

namespace Ticket4S.Services.Pagamento.Model
{
    public class CobrancaViaCartaoDeCredito
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public CartaoDeCredito CartaoDeCredito { get; set; }

        public EnderecoDeCobranca EnderecoDeCobranca { get; set; }

        [Required]
        public decimal? Valor { get; set; }
    }

    public class CartaoDeCredito
    {
        [Required]
        public string NomeDoDono { get; set; }
        [Required]
        public SecureString Numero { get; set; }

        [Required]
        public SecureString CodigoDeSeguranca { get; set; }

        [Required]
        public int ExpiracaoMes { get; set; }
        [Required]
        public int ExpiracaoAno { get; set; }

        [Required]
        public Bandeira? Bandeira { get; set; }
    }

    public enum Bandeira
    {
        Visa = 1,
        Mastercard = 2,
        Hipercard = 3,
        Amex = 4,
        Diners = 5,
        Elo = 6,
        Aura = 7,
        Discover = 8,
        CasaShow = 9,
        Havan = 10,
        HugCard = 11,
        AndarAki = 12,
        LeaderCard = 13
    }

    public class EnderecoDeCobranca
    {
        public string Pais { get; set; }
        public string UF { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string CEP { get; set; }
    }
}