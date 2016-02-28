using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ticket4S.Entity.User;

namespace Ticket4S.Entity.Evento
{

    [Table("Eventos", Schema = "Evento")]
    public class Evento
    {
        [Key]
        public virtual Guid Id { get; set; }

        [Required, StringLength(256)]
        public virtual string Nome { get; set; }

        [Required, StringLength(16)]
        public virtual string NomeCurto { get; set; }

        [Required]
        public virtual Local Local { get; set; }

        public virtual ICollection<TipoDeIngressoDoEvento> TipoDeIngressos { get; protected set; } = new List<TipoDeIngressoDoEvento>();

        
        [Timestamp, Column("_rowVersion")]
        public virtual byte[] RowVersion { get; set; }
    }
}
