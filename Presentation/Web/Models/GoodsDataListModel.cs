using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class GoodsDataListModel
    {
        public string Name { get; set; }

        public IList<GoodsDataModel> GoodsData { get; set; }
    }
}