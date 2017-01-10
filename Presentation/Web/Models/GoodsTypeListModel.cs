using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class GoodsTypeListModel
    {
        [DisplayName("商品类别名")]
        public string Name { get; set; }

        public IList<GoodsTypeModel> GoodsType { get; set; }
    }
}