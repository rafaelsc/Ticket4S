using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ticket4S.Entity;
using Ticket4S.Entity.Evento;
using Ticket4S.Entity.GatewayPagamento;
using Ticket4S.Entity.User;
using Ticket4S.Entity.Geo;

namespace Ticket4S.Web.Models
{
    public class Ticket4SDbContext : IdentityDbContext<Usuario>
    {
        //Evento
        public virtual DbSet<Evento> Evento { get; set; }
        public virtual DbSet<Local> LocalDeEvento { get; set; }

        //GatwayPagemento
        public virtual DbSet<CartaoDeCreditoSalvo> CartaoDeCreditoSalvo { get; set; }
        public virtual DbSet<HistoricoDeTransacoes> HistoricoDeTransacoes { get; set; }
        
        //User
        public virtual DbSet<Endereco> Enderecos { get; set; }

        //Geo
        public virtual DbSet<UF> UFs { get; set; }
        public virtual DbSet<Cidade> Cidades { get; set; }
        public virtual DbSet<Bairro> Bairros { get; set; }

        ///////////////////////////////////////////////////////////////////////////

        public Ticket4SDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static Ticket4SDbContext Create()
        {
            return new Ticket4SDbContext();
        }
    }
}