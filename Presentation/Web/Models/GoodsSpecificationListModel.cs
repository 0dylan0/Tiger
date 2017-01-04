using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class GoodsSpecificationListModel
    {
        public string Name { get; set; }

        public IList<GoodsSpecificationModel> GoodsSpecification { get; set; }
    }
}