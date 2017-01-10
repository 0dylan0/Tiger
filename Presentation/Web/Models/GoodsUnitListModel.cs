using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class GoodsUnitListModel
    {
        [DisplayName("货物单位名")]
        public string Name { get; set; }

        public IList<GoodsUnitModel> GoodsUnit { get; set; }
    }
}