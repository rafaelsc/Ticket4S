using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Ticket4S.Entity.Event;
using Ticket4S.Entity.User;

namespace Ticket4S.Entity.Purchase
{
    [Table("PurchaseOrders", Schema = "Purchase")]
    public class PurchaseOrder
    {
        [Key]
        public virtual Guid Id { get; set; }

        [Index]
        [Required]
        [DataType(DataType.DateTime)]
        public virtual DateTimeOffset OrderDateTime { get; set; }

        [Index]
        [ForeignKey(nameof(BuyerUser))]
        public virtual string BuyerUserId { get; set; }
        public virtual User.User BuyerUser { get; set; }

        [Required]
        public virtual bool UserRequestToSaveCreditCard { get; set; }

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

        [Index]
        [Required]
        public virtual Condition Condition { get; set; } = Condition.WaitingProcessing;


        [Column("_createdAt")]
        public virtual DateTimeOffset CreatedAt { get; set; }
        [Column("_changedAt")]
        public virtual DateTimeOffset ChangedAt { get; set; }
        [Timestamp, Column("_rowVersion")]
        public virtual byte[] RowVersion { get; set; }


        public override string ToString() => $"Id: {Id}, OrderDateTime: {OrderDateTime}, BuyerUserId: {BuyerUserId}, BoughtTicketId: {BoughtTicketId}, Condition: {Condition}";
    }

    public enum Condition
    {
        WaitingProcessing = 0,
        ProccessedSuccessful = 10,
        ProccessedFail = 20
    }
}
