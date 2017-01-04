using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class GoodsUnitListModel
    {
        public string Name { get; set; }

        public IList<GoodsUnitModel> GoodsUnit { get; set; }
    }
}