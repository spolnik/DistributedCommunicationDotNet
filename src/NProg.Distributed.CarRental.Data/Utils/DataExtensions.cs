using System.Collections.Generic;
using System.Linq;

namespace NProg.Distributed.CarRental.Data.Utils
{
    public static class DataExtensions
    {
        public static IEnumerable<T> ToFullyLoaded<T>(this IQueryable<T> query)
        {
            return query.ToArray().ToList();
        }
    }
}