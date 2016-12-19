using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class UriExtensions
    {
        public static string GetSchemeAndAuthorityPath(this Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException(nameof(uri));
            }

            return $"{uri.Scheme}://{uri.Authority}";
        }
    }
}
