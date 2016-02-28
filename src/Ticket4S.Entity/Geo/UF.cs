using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket4S.Entity.Geo
{
    [Table("UFs", Schema = "Geo")]
    public class UF
    {
        [Key, StringLength(32)]
        public string Id { get; set; }

        [Required, StringLength(255)]
        public string Nome { get; set; }

        [Required, StringLength(8)]
        public string Abreviacao { get; set; }

        [Required]
        public bool Habilitado { get; set; } = false;

        [Required]
        public string ContryIsoCode { get; set; }

        public virtual ICollection<Cidade> Cidades { get; protected set; } = new List<Cidade>();
    }
}