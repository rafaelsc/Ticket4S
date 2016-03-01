using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;

namespace Ticket4S.Entity.Event
{

    [Table("Events", Schema = "Event")]
    public class Event
    {
        [Key]
        public virtual Guid Id { get; set; }

        [Required, StringLength(256)]
        public virtual string Name { get; set; }

        [Required, StringLength(32)]
        public virtual string ShortName { get; set; }

        [Required]
        [ForeignKey(nameof(EventPlace))]
        public virtual Guid? EventPlaceId { get; set; }
        public virtual EventPlace EventPlace { get; set; }

        public virtual ICollection<EventTicketType> TicketsTypes { get; protected set; } = new List<EventTicketType>();

        [Required]
        [DataType(DataType.DateTime)]
        public virtual DateTimeOffset? BeginningOfSales { get; set; }

        [CanBeNull]
        [DataType(DataType.DateTime)]
        public virtual DateTimeOffset? EndOfSales { get; set; }

        public virtual bool Active { get; set; } = false;

        [Column("_createdAt")]
        public virtual DateTimeOffset CreatedAt { get; set; }
        [Column("_changedAt")]
        public virtual DateTimeOffset ChangedAt { get; set; }
        [Timestamp, Column("_rowVersion")]
        public virtual byte[] RowVersion { get; set; }
    }
}
