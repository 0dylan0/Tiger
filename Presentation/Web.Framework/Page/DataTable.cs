using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Framework.Page
{
    public class DataTable<T>
    {
        public int Draw { get; set; }
        public int RecordsTotal { get; set; }
        public int RecordsFiltered { get; set; }
        public IList<T> Data { get; set; }
    }
}
