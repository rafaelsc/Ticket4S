using System;
using System.Data.Entity;
using System.Linq;

namespace Ticket4S.Entity.Querys
{
    public static class EventoQuerys
    {
        public static IQueryable<Evento.Evento> Ativos(this IQueryable<Evento.Evento> eventos)
        {
            return eventos.Where(e => e.Habilitdo);
        }

        public static IQueryable<Evento.Evento> ListarEventosDisponiveisParaCompra(this IQueryable<Evento.Evento> eventos)
        {
            var date = DateTimeOffset.Now;
            return eventos.ListarEventosDisponiveisParaCompra(date);
        }

        public static IQueryable<Evento.Evento> ListarEventosDisponiveisParaCompra(this IQueryable<Evento.Evento> eventos, DateTimeOffset naData)
        {
            var result = from ev in eventos.Ativos()
                         where naData >= ev.InicioDasVendas
                         where ev.TerminoDasVendas == null || ev.TerminoDasVendas <= naData
                         orderby ev.Nome
                         select ev;

            return result;
        }
    }

    public static class TipoDeIngressoDoEventoQuerys
    {
        public static IQueryable<Evento.TipoDeIngressoDoEvento> Ativos(this IQueryable<Evento.TipoDeIngressoDoEvento> tipoDeIngressos)
        {
            return tipoDeIngressos.Where(e => e.Habilitado);
        }

        public static IQueryable<Evento.TipoDeIngressoDoEvento> OrdemPadrao(this IQueryable<Evento.TipoDeIngressoDoEvento> tipoDeIngressos)
        {
            var result = from t in tipoDeIngressos
                         orderby t.OrdemDeExibicao
                         orderby t.Nome
                         select t;

            return result;
        }
    }

}
