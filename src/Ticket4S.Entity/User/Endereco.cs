using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ticket4S.Entity.Geo;

namespace Ticket4S.Entity.User
{
    public class Endereco
    {
        [Key]
        public virtual Guid Id { get; set; }
        
        [ForeignKey(nameof(UF))]
        public virtual string UFId { get; set; }
        public virtual UF UF { get; set; }

        [ForeignKey(nameof(Cidade))]
        public virtual string CidadeId { get; set; }
        public virtual Cidade Cidade { get; set; }

        [ForeignKey(nameof(Bairro))]
        public virtual string BairroId { get; set; }
        public virtual Bairro Bairro { get; set; }

        public virtual string CEP { get; set; }

        public virtual string NomeDaRua { get; set; }
        public virtual string NumeroDaRua { get; set; }
        public virtual string ComplementoDaRua { get; set; }
        
        

        [ForeignKey(nameof(Usuario))]
        public virtual string UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }


        [Timestamp, Column("_rowVersion")]
        public virtual byte[] RowVersion { get; set; }
    }
}