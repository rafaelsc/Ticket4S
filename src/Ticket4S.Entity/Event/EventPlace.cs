using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ticket4S.Entity.Geo;

namespace Ticket4S.Entity.Event
{
    [Table("Places", Schema = "Event")]
    public class EventPlace
    {
        [Key]
        public virtual Guid Id { get; set; }

        [Required, StringLength(256)]
        public virtual string Name { get; set; }

        [Required, StringLength(32)]
        public virtual string ShortName { get; set; }

        [Required]
        [ForeignKey(nameof(State))]
        public virtual string StateId { get; set; }
        public virtual State State { get; set; }

        [Required]
        [ForeignKey(nameof(City))]
        public virtual string CityId { get; set; }
        public virtual City City { get; set; }

        [Required]
        [ForeignKey(nameof(District))]
        public virtual string DistrictId { get; set; }
        public virtual District District { get; set; }


        [Required, StringLength(8)]
        public virtual string ZipCode { get; set; }

        [StringLength(256)]
        public virtual string Street { get; set; }

        [StringLength(32)]
        public virtual string StreetNumber { get; set; }

        [StringLength(128)]
        public virtual string StreetComplement { get; set; }

        [Timestamp, Column("_rowVersion")]
        public virtual byte[] RowVersion { get; set; }
    }
}