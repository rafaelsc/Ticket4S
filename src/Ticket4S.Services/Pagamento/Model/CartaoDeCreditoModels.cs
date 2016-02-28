using System;
using System.ComponentModel.DataAnnotations;
using System.Security;
using JetBrains.Annotations;
using Ticket4S.Entity;
using Ticket4S.Entity.User;

namespace Ticket4S.Services.Pagamento.Model
{
    public class CobrancaViaCartaoDeCredito
    {
        [Key]
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public CartaoDeCredito CartaoDeCredito { get; set; }

        public EnderecoDeCobranca EnderecoDeCobranca { get; set; }

        [DataType(DataType.Currency)]
        [Required, Range(0.01d, 1000000d)] //1 Milhao
        public decimal? Valor { get; set; }


        public override string ToString() => $"Id: {Id}, CartaoDeCredito: {CartaoDeCredito}, Valor: {Valor}";
    }

    public class CartaoDeCredito
    {
        [Required]
        public string NomeDoDono { get; set; }

        [DataType(DataType.CreditCard)]
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


        public override string ToString() => $"Bandeira: {Bandeira}, NomeDoDono: {NomeDoDono}";
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


        public override string ToString() => $"Pais: {Pais}, UF: {UF}, Cidade: {Cidade}, Bairro: {Bairro}, Rua: {Rua}, Numero: {Numero}, Complemento: {Complemento}, CEP: {CEP}";
    }
}