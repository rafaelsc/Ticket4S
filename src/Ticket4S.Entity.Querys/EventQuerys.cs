using System;
using System.Data.Entity;
using System.Linq;

namespace Ticket4S.Entity.Querys
{
    public static class EventQuerys
    {
        public static IQueryable<Event.Event> OnlyActives(this IQueryable<Event.Event> events)
        {
            return events.Where(e => e.Active);
        }

        public static IQueryable<Event.Event> ListAvailableEventsWithTicket(this IQueryable<Event.Event> events)
        {
            var date = DateTimeOffset.Now;
            return events.ListAvailableEventsWithTicket(date);
        }

        public static IQueryable<Event.Event> ListAvailableEventsWithTicket(this IQueryable<Event.Event> events, DateTimeOffset onDate)
        {
            var result = from ev in events.OnlyActives()
                                    .Include(e => e.TicketsTypes)
                         where onDate >= ev.BeginningOfSales
                         where ev.EndOfSales == null || ev.EndOfSales <= onDate
                         where ev.TicketsTypes.Any()
                         orderby ev.Name
                         select ev;

            return result;
        }
    }

    public static class EventTicketTypesQuerys
    {
        public static IQueryable<Event.EventTicketType> OnlyActives(this IQueryable<Event.EventTicketType> eventTicketTypes)
        {
            return eventTicketTypes.Where(e => e.Available);
        }

        public static IQueryable<Event.EventTicketType> InDefaultOrder(this IQueryable<Event.EventTicketType> eventTicketTypes)
        {
            var result = from t in eventTicketTypes
                         orderby t.ViewOrder
                         orderby t.Name
                         select t;

            return result;
        }
    }

}
