using System.Collections.Generic;

namespace Ticket4S.Extensions
{
    public static class CollectionExtensions
    {
        public static void AddAll<T>(this ICollection<T> col, IEnumerable<T> itens) 
        {
            foreach (var item in itens)
            {
                col.Add(item);
            }
        }
    }
}
