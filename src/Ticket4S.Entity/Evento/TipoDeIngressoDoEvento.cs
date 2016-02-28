using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ticket4S.Entity.Evento
{
    [Table("TiposDeIngressoDoEvento", Schema = "Evento")]
    public class TipoDeIngressoDoEvento
    {
        [Key]
        public virtual Guid Id { get; set; }

        [Required, StringLength(256)]
        public virtual string Nome { get; set; }

        [DataType(DataType.Currency)]
        [Required, Range(typeof(decimal), "0", "1000000")] //1 Milhao
        public decimal Valor { get; set; } = 0M;

        [Required]
        [ForeignKey(nameof(Evento))]
        public virtual string EventoId { get; set; }
        public virtual Evento Evento { get; set; }

        [Timestamp, Column("_rowVersion")]
        public virtual byte[] RowVersion { get; set; }
    }
}