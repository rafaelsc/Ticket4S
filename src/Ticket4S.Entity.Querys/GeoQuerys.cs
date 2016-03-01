using System.Data.Entity;
using System.Linq;
using Ticket4S.Entity.Geo;

namespace Ticket4S.Entity.Querys
{
    public static class UFsQuerys
    {
        public static IQueryable<State> Ativos(this IQueryable<State> ufs)
        {
            return ufs.Where(e => e.Available);
        }

        public static IQueryable<State> OrdemPadrao(this IQueryable<State> ufs)
        {
            var result = from t in ufs
                orderby t.Name
                select t;

            return result;
        }

        public static IQueryable<State> ListarParaCombo(this IQueryable<State> ufs)
        {
            var result = from t in ufs.AsNoTracking().Ativos().OrdemPadrao()
                select t;

            return result;
        }
    }

    public static class CidadeQuerys
    {
        public static IQueryable<City> Ativos(this IQueryable<City> cidades)
        {
            return cidades.Where(e => e.Available);
        }

        public static IQueryable<City> OrdemPadrao(this IQueryable<City> cidades)
        {
            var result = from t in cidades
                         orderby t.Name
                         select t;

            return result;
        }

        public static IQueryable<City> ListarParaCombo(this IQueryable<City> cidades)
        {
            var result = from t in cidades.AsNoTracking().Ativos().OrdemPadrao()
                         select t;

            return result;
        }
    }

    public static class BairroQuerys
    {
        public static IQueryable<District> Ativos(this IQueryable<District> bairros)
        {
            return bairros.Where(e => e.Available);
        }

        public static IQueryable<District> OrdemPadrao(this IQueryable<District> bairros)
        {
            var result = from t in bairros
                         orderby t.Name
                         select t;

            return result;
        }

        public static IQueryable<District> ListarParaCombo(this IQueryable<District> bairros)
        {
            var result = from t in bairros.AsNoTracking().Ativos().OrdemPadrao()
                         select t;

            return result;
        }
    }
}