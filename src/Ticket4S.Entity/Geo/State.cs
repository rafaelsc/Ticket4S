using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket4S.Entity.Geo
{
    [Table("States", Schema = "Geo")]
    public class State
    {
        [Key, StringLength(32)]
        public string Id { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        [Required, StringLength(8)]
        public string Abbreviation { get; set; }

        [Required]
        public bool Available { get; set; } = false;

        [Required]
        public string ContryIsoCode { get; set; }

        public virtual ICollection<City> Cities { get; protected set; } = new List<City>();
    }
}