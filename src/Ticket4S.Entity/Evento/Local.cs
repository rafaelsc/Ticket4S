using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ticket4S.Entity.Geo;

namespace Ticket4S.Entity.Evento
{
    [Table("Locais", Schema = "Evento")]
    public class Local
    {
        [Key]
        public virtual Guid Id { get; set; }


        [Required, StringLength(256)]
        public virtual string Nome { get; set; }

        [Required, StringLength(16)]
        public virtual string NomeCurto { get; set; }

        [Required]
        [ForeignKey(nameof(UF))]
        public virtual string UFId { get; set; }
        public virtual UF UF { get; set; }

        [Required]
        [ForeignKey(nameof(Cidade))]
        public virtual string CidadeId { get; set; }
        public virtual Cidade Cidade { get; set; }

        [Required]
        [ForeignKey(nameof(Bairro))]
        public virtual string BairroId { get; set; }
        public virtual Bairro Bairro { get; set; }


        [Required, StringLength(8)]
        public virtual string CEP { get; set; }

        [StringLength(256)]
        public virtual string NomeDaRua { get; set; }

        [StringLength(32)]
        public virtual string NumeroDaRua { get; set; }

        [StringLength(128)]
        public virtual string ComplementoDaRua { get; set; }

        [Timestamp, Column("_rowVersion")]
        public virtual byte[] RowVersion { get; set; }
    }
}