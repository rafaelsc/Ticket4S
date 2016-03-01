using System.Data.Entity;
using System.Linq;
using Ticket4S.Entity.Geo;

namespace Ticket4S.Entity.Querys
{
    public static class StatesQuerys
    {
        public static IQueryable<State> OnlyAvailables(this IQueryable<State> states)
        {
            return states.Where(e => e.Available);
        }

        public static IQueryable<State> InDefaultOrder(this IQueryable<State> states)
        {
            var result = from t in states
                orderby t.Name
                select t;

            return result;
        }

        public static IQueryable<State> ListForDropDown(this IQueryable<State> states)
        {
            var result = from t in states.AsNoTracking().OnlyAvailables().InDefaultOrder()
                select t;

            return result;
        }
    }

    public static class CidadeQuerys
    {
        public static IQueryable<City> OnlyAvailables(this IQueryable<City> cities)
        {
            return cities.Where(e => e.Available);
        }

        public static IQueryable<City> InDefaultOrder(this IQueryable<City> cities)
        {
            var result = from t in cities
                         orderby t.Name
                         select t;

            return result;
        }

        public static IQueryable<City> ListForDropDown(this IQueryable<City> cities)
        {
            var result = from t in cities.AsNoTracking().OnlyAvailables().InDefaultOrder()
                         select t;

            return result;
        }
    }

    public static class BairroQuerys
    {
        public static IQueryable<District> OnlyAvailables(this IQueryable<District> districts)
        {
            return districts.Where(e => e.Available);
        }

        public static IQueryable<District> InDefaultOrder(this IQueryable<District> districts)
        {
            var result = from t in districts
                         orderby t.Name
                         select t;

            return result;
        }

        public static IQueryable<District> ListForDropDown(this IQueryable<District> districts)
        {
            var result = from t in districts.AsNoTracking().OnlyAvailables().InDefaultOrder()
                         select t;

            return result;
        }
    }
}