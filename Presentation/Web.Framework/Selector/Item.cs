using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Framework.Selector
{
    public class Item
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string CategoryCode { get; set; }

        public string CategoryName { get; set; }
    }

    public class Item<T> : Item
    {
        public T Data { get; set; }
    }
}
