using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ticket4S.Entity.User;

namespace Ticket4S.Entity.GatewayPagamento
{
    [Table("CartaoDeCreditoSalvo", Schema = "GatewayPagamento")]
    public class CartaoDeCreditoSalvo
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public virtual string IdDoCartaoNoGateway { get; set; }

        [Required]
        public Bandeira? Bandeira { get; set; }

        [Required, StringLength(64)]
        public string NumeroCartaoMascarado { get; set; }

        [ForeignKey(nameof(Usuario))]
        public virtual string UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}