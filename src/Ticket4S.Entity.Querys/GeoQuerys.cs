using System.Data.Entity;
using System.Linq;
using Ticket4S.Entity.Geo;

namespace Ticket4S.Entity.Querys
{
    public static class UFsQuerys
    {
        public static IQueryable<UF> Ativos(this IQueryable<UF> ufs)
        {
            return ufs.Where(e => e.Habilitado);
        }

        public static IQueryable<UF> OrdemPadrao(this IQueryable<UF> ufs)
        {
            var result = from t in ufs
                orderby t.Nome
                select t;

            return result;
        }

        public static IQueryable<UF> ListarParaCombo(this IQueryable<UF> ufs)
        {
            var result = from t in ufs.AsNoTracking().Ativos().OrdemPadrao()
                select t;

            return result;
        }
    }

    public static class CidadeQuerys
    {
        public static IQueryable<Cidade> Ativos(this IQueryable<Cidade> cidades)
        {
            return cidades.Where(e => e.Habilitado);
        }

        public static IQueryable<Cidade> OrdemPadrao(this IQueryable<Cidade> cidades)
        {
            var result = from t in cidades
                         orderby t.Nome
                         select t;

            return result;
        }

        public static IQueryable<Cidade> ListarParaCombo(this IQueryable<Cidade> cidades)
        {
            var result = from t in cidades.AsNoTracking().Ativos().OrdemPadrao()
                         select t;

            return result;
        }
    }

    public static class BairroQuerys
    {
        public static IQueryable<Bairro> Ativos(this IQueryable<Bairro> bairros)
        {
            return bairros.Where(e => e.Habilitado);
        }

        public static IQueryable<Bairro> OrdemPadrao(this IQueryable<Bairro> bairros)
        {
            var result = from t in bairros
                         orderby t.Nome
                         select t;

            return result;
        }

        public static IQueryable<Bairro> ListarParaCombo(this IQueryable<Bairro> bairros)
        {
            var result = from t in bairros.AsNoTracking().Ativos().OrdemPadrao()
                         select t;

            return result;
        }
    }
}