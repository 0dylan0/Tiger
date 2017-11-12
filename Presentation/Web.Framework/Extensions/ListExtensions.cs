using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Framework.Extensions
{
    public static class ListExtensions
    {
        public static IEnumerable<T> CheckNull<T>(this IEnumerable<T> list)
        {
            return list == null ? new List<T>() : list;
        }
    }
}
