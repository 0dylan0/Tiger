using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class SalesShipmentsDataListModel
    {
        [DisplayName("商品名")]
        public string Name { get; set; }

        [DisplayName("显示退货信息")]
        public bool ShowInactive { get; set; }

        public IList<SalesShipmentsDataModel> SalesShipmentsData { get; set; }
    }
}