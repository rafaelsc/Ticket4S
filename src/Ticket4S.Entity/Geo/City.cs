using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket4S.Entity.Geo
{
    [Table("Cities", Schema = "Geo")]
    public class City
    {
        [Key, StringLength(32)]
        public string Id { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        [Required]
        public bool Available { get; set; } = false;

        [Required]
        [ForeignKey(nameof(State))]
        public string StateId { get; set; }
        public virtual State State { get; set; }

        public virtual ICollection<District> Districts { get; protected set; } = new List<District>();
    }
}
