using System;
using System.Data.Entity;
using System.Linq;

namespace Ticket4S.Entity.Querys
{
    public static class EventoQuerys
    {
        public static IQueryable<Event.Event> Ativos(this IQueryable<Event.Event> eventos)
        {
            return eventos.Where(e => e.Active);
        }

        public static IQueryable<Event.Event> ListarEventosDisponiveisParaCompra(this IQueryable<Event.Event> eventos)
        {
            var date = DateTimeOffset.Now;
            return eventos.ListarEventosDisponiveisParaCompra(date);
        }

        public static IQueryable<Event.Event> ListarEventosDisponiveisParaCompra(this IQueryable<Event.Event> eventos, DateTimeOffset naData)
        {
            var result = from ev in eventos.Ativos()
                         where naData >= ev.BeginningOfSales
                         where ev.EndOfSales == null || ev.EndOfSales <= naData
                         orderby ev.Name
                         select ev;

            return result;
        }
    }

    public static class TipoDeIngressoDoEventoQuerys
    {
        public static IQueryable<Event.EventTicketType> Ativos(this IQueryable<Event.EventTicketType> tipoDeIngressos)
        {
            return tipoDeIngressos.Where(e => e.Available);
        }

        public static IQueryable<Event.EventTicketType> OrdemPadrao(this IQueryable<Event.EventTicketType> tipoDeIngressos)
        {
            var result = from t in tipoDeIngressos
                         orderby t.ViewOrder
                         orderby t.Name
                         select t;

            return result;
        }
    }

}
