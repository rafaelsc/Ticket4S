using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ticket4S.Entity.User;

namespace Ticket4S.Entity.GatewayPayment
{
    [Table("SavedCreditCards", Schema = "GatewayPayment")]
    public class SavedCreditCard
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public virtual string IdOfSavedCardInTheGateway { get; set; }

        [Required]
        public CreditCardBrand? CreditCardBrand { get; set; }

        [Required, StringLength(24)]
        public string MaskedCreditCardNumber { get; set; }

        [ForeignKey(nameof(User))]
        public virtual string UserId { get; set; }
        public virtual User.User User { get; set; }

        [Column("_createdAt")]
        public virtual DateTimeOffset CreatedAt { get; set; }
    }
}