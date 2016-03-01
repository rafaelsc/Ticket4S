using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket4S.Entity.Event
{
    [Table("EventTicketTypes", Schema = "Event")]
    public class EventTicketType
    {
        [Key]
        public virtual Guid Id { get; set; }

        [Required, StringLength(256)]
        public virtual string Name { get; set; }

        [DataType(DataType.Currency)]
        [Required, Range(0.01d, 1000000d)] //1 Milhao
        public decimal Value { get; set; } = 0M;

        public virtual byte ViewOrder { get; set; }

        public virtual bool Available { get; set; } = true;

        [Required]
        [ForeignKey(nameof(Event))]
        public virtual Guid? EventId { get; set; }
        public virtual Event Event { get; set; }

        [Timestamp, Column("_rowVersion")]
        public virtual byte[] RowVersion { get; set; }
    }
}