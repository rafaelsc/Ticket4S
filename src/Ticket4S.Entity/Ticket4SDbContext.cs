using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ticket4S.Entity.Purchase;
using Ticket4S.Entity.Event;
using Ticket4S.Entity.GatewayPayment;
using Ticket4S.Entity.Geo;
using Ticket4S.Entity.User;

namespace Ticket4S.Entity
{
    public class Ticket4SDbContext : IdentityDbContext<User.User>
    {
        //Evento
        public virtual DbSet<Event.Event> Event { get; set; }
        public virtual DbSet<Event.EventTicketType> EventTicketTypes { get; set; }

        public virtual DbSet<EventPlace> EventsPlaces { get; set; }

        //Compra
        public virtual DbSet<PurchaseOrder> PedidosDeCompras { get; set; }

        //GatwayPagemento
        public virtual DbSet<SavedCreditCard> CartaoDeCreditoSalvo { get; set; }
        public virtual DbSet<TransactionHistory> HistoricoDeTransacoes { get; set; }
        
        //User
        public virtual DbSet<Address> Addresses { get; set; }

        //Geo
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<District> Neighborhoods { get; set; }

        ///////////////////////////////////////////////////////////////////////////

        public Ticket4SDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static Ticket4SDbContext Create()
        {
            return new Ticket4SDbContext();
        }

        ///////////////////////////////////////////////////////////////////////////
    }
}