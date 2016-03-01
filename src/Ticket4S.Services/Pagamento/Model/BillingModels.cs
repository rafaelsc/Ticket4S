using System;
using System.ComponentModel.DataAnnotations;
using System.Security;
using JetBrains.Annotations;
using Ticket4S.Entity;
using Ticket4S.Entity.User;

namespace Ticket4S.Services.Pagamento.Model
{
    public class BillingWithCreditCard
    {
        [Key]
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public CreditCardInfo CreditCard { get; set; }

        public BillingAddress BillingAddress { get; set; }

        [DataType(DataType.Currency)]
        [Required, Range(0.01d, 1000000d)] //1 Milhao
        public decimal? Value { get; set; }


        public override string ToString() => $"Id: {Id}, CreditCard: {CreditCard}, Valor: {Value}";
    }

    public class CreditCardInfo
    {
        [Required]
        public string HolderName { get; set; }

        [Required]
        [DataType(DataType.CreditCard)]
        public SecureString CreditCardNumber { get; set; }

        [Required]
        public SecureString SecurityCode { get; set; }

        [Required]
        public int ExpMonth { get; set; }
        [Required]
        public int ExpYear { get; set; }

        [Required]
        public CreditCardBrand? CreditCardBrand { get; set; }


        public override string ToString() => $"CreditCardBrand: {CreditCardBrand}, HolderName: {HolderName}";
    }
    
    public class BillingAddress
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string ZipCode { get; set; }


        public override string ToString() => $"Country: {Country}, State: {State}, City: {City}, District: {District}, Street: {Street}, Number: {Number}, Complement: {Complement}, ZipCode: {ZipCode}";
    }
}