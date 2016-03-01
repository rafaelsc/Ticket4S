using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using Ticket4S.Entity.Event;

namespace Ticket4S.Entity.GatewayPayment
{
    [Table("TransactionHistory", Schema = "GatewayPayment")]
    public class TransactionHistory
    {
        [Key]
        public virtual Guid Id { get; set; }

        [Index]
        [Required]
        [DataType(DataType.DateTime)]
        public virtual DateTimeOffset TransactionDateTime { get; set; }

        [Index]
        [ForeignKey(nameof(BuyerUser))]
        public virtual string BuyerUserId { get; set; }
        public virtual User.User BuyerUser { get; set; }

        [Index]
        [ForeignKey(nameof(BoughtEvent))]
        public virtual Guid? BoughtEventId { get; set; }
        public virtual Event.Event BoughtEvent { get; set; }

        
        [ForeignKey(nameof(BoughtTicket))]
        public virtual Guid? BoughtTicketId { get; set; }
        public virtual EventTicketType BoughtTicket { get; set; }

        [DataType(DataType.Currency)]
        [Required, Range(0.01d, 1000000d)] //1 Milhao
        public virtual decimal BilledValue { get; set; }

        [Required]
        public virtual bool UserRequestToSaveCreditCard { get; set; }

        [Required]
        public virtual bool PaymentBilledSuccessful { get; set; }

        [CanBeNull]
        public virtual string OrderIdInTheGatewaySystem { get; set; }

        [StringLength(512)]
        public virtual string OperationMessage { get; set; }

        [DataType(DataType.Text)]
        public virtual string DebugRawData { get; set; }


        [Column("_createdAt")]
        public virtual DateTimeOffset CreatedAt { get; set; }
    }
}