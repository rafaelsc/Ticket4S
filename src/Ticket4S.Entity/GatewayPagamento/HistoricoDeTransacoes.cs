using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using Ticket4S.Entity.Evento;
using Ticket4S.Entity.User;

namespace Ticket4S.Entity.GatewayPagamento
{
    [Table("HistoricoDeTransacoes", Schema = "GatewayPagamento")]
    public class HistoricoDeTransacoes
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
        public virtual Guid? EventoAdquiridoId { get; set; }
        public virtual Evento.Evento EventoAdquirido { get; set; }

        
        [ForeignKey(nameof(IngressoAdquirido))]
        public virtual Guid? IngressoAdquiridoId { get; set; }
        public virtual TipoDeIngressoDoEvento IngressoAdquirido { get; set; }

        [Required]
        public virtual bool SolicitadoSalvarOCartaoDeCredito { get; set; }

        [Required]
        public virtual bool PagamentoCobradoComSucesso { get; set; }

        [CanBeNull]
        public virtual string IdDoPedidoNoSistemaDePagamento { get; set; }

        [StringLength(512)]
        public virtual string MessagemDeRespostaDaOperacao { get; set; }

        [DataType(DataType.Text)]
        public virtual string DebugRawData { get; set; }


        [Column("_criadoEm")]
        public virtual DateTimeOffset CriadoEm { get; set; }
    }
}