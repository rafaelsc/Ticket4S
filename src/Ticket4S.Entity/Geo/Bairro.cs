using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket4S.Entity.Geo
{
    [Table("Bairros", Schema = "Geo")]
    public class Bairro
    {
        [Key, StringLength(32)]
        public string Id { get; set; }

        [Required, StringLength(255)]
        public string Nome { get; set; }

        [Required]
        public bool Habilitado { get; set; } = false;

        [Required]
        [ForeignKey(nameof(Cidade))]
        public string CidadeId { get; set; }
        
        public virtual Cidade Cidade { get; set; }
    }
}