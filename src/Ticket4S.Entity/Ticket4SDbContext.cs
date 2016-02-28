using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ticket4S.Entity;
using Ticket4S.Entity.Evento;
using Ticket4S.Entity.User;
using Ticket4S.Entity.Geo;

namespace Ticket4S.Web.Models
{
    public class Ticket4SDbContext : IdentityDbContext<Usuario>
    {
        //Evento
        public DbSet<Evento> Evento { get; set; }
        public DbSet<Local> LocalDeEvento { get; set; }

        //User
        public DbSet<Endereco> Enderecos { get; set; }

        //Geo
        public DbSet<UF> UFs { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Bairro> Bairros { get; set; }

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