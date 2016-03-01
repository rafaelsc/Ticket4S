using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket4S.Entity.Geo
{
    [Table("Districts", Schema = "Geo")]
    public class District
    {
        [Key, StringLength(32)]
        public string Id { get; set; }

        [Required, StringLength(255)]
        public string Name { get; set; }

        [Required]
        public bool Available { get; set; } = false;

        [Required]
        [ForeignKey(nameof(City))]
        public string CityId { get; set; }
        
        public virtual City City { get; set; }
    }
}