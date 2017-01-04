using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class GoodsTypeListModel
    {
        public string Name { get; set; }

        public IList<GoodsTypeModel> GoodsType { get; set; }
    }
}