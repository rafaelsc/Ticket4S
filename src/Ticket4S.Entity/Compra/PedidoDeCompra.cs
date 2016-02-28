using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Ticket4S.Entity.Evento;
using Ticket4S.Entity.User;

namespace Ticket4S.Entity.Compra
{
    [Table("PedidosDeCompras", Schema = "Compras")]
    public class PedidoDeCompra
    {
        [Key]
        public virtual Guid Id { get; set; }

        [Index]
        [Required]
        [DataType(DataType.DateTime)]
        public virtual DateTimeOffset DataHoraEvento { get; set; }

        [Index]
        [ForeignKey(nameof(UsuarioComprador))]
        public virtual string UsuarioId { get; set; }
        public virtual Usuario UsuarioComprador { get; set; }

        [Index]
        [ForeignKey(nameof(EventoAdquirido))]
        public virtual string EventoAdquiridoId { get; set; }
        public virtual Evento.Evento EventoAdquirido { get; set; }


        [ForeignKey(nameof(IngressoAdquirido))]
        public virtual string IngressoAdquiridoId { get; set; }
        public virtual TipoDeIngressoDoEvento IngressoAdquirido { get; set; }

        [DataType(DataType.Currency)]
        [Required, Range(0.01d, 1000000d)] //1 Milhao
        public virtual decimal ValorCobrado { get; set; }

        [Index]
        [Required]
        public virtual Situacao Situacao { get; set; } = Situacao.AguardandoProcessamento;

        [Column("_criadoEm")]
        public virtual DateTimeOffset CriadoEm { get; set; }
        [Column("_modificadoEm")]
        public virtual DateTimeOffset ModificadoEm { get; set; }
        [Timestamp, Column("_rowVersion")]
        public virtual byte[] RowVersion { get; set; }
    }

    public enum Situacao
    {
        AguardandoProcessamento,
        Processado
    }
}
