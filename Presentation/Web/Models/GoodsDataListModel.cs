using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class GoodsDataListModel
    {
        [DisplayName("商品名")]
        public string Name { get; set; }

        public IList<GoodsDataModel> GoodsData { get; set; }
    }
}