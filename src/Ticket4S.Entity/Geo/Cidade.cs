using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket4S.Entity.Geo
{
    [Table("Cidades", Schema = "Geo")]
    public class Cidade
    {
        [Key, StringLength(32)]
        public string Id { get; set; }

        [Required, StringLength(255)]
        public string Nome { get; set; }

        [Required]
        public bool Habilitado { get; set; } = false;

        [Required]
        [ForeignKey(nameof(UF))]
        public string UFId { get; set; }
        public virtual UF UF { get; set; }

        public virtual ICollection<Bairro> Neighborhoods { get; set; }
    }
}
