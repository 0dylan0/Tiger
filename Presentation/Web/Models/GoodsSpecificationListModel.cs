using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class GoodsSpecificationListModel
    {
        [DisplayName("商品规格名")]
        public string Name { get; set; }

        public IList<GoodsSpecificationModel> GoodsSpecification { get; set; }
    }
}