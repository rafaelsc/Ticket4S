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

        [Index()]
        [Required, StringLength(32)]
        public virtual string ShortName { get; set; }

        [Required]
        [ForeignKey(nameof(EventPlace))]
        public virtual Guid? EventPlaceId { get; set; }
        public virtual EventPlace EventPlace { get; set; }

        public virtual ICollection<EventTicketType> TicketsTypes { get; set; } = new List<EventTicketType>();

        [Index("periodOfSales", 2)]
        [Required]
        [DataType(DataType.DateTime)]
        public virtual DateTimeOffset? BeginningOfSales { get; set; }

        [Index("periodOfSales", 3)]
        [CanBeNull]
        [DataType(DataType.DateTime)]
        public virtual DateTimeOffset? EndOfSales { get; set; }

        [Index("periodOfSales", 1)]
        public virtual bool Active { get; set; } = false;

        [Column("_createdAt")]
        public virtual DateTimeOffset CreatedAt { get; set; }
        [Column("_changedAt")]
        public virtual DateTimeOffset ChangedAt { get; set; }
        [Timestamp, Column("_rowVersion")]
        public virtual byte[] RowVersion { get; set; }
    }
}
