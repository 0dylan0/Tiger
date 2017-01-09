using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class PurchaseDataListModel
    {
        [DisplayName("商品名")]
        public string Name { get; set; }

        public IList<PurchaseDataModel> PurchaseData { get; set; }
    }
}