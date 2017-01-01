using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class PurchaseDataListModel
    {
        public string Name { get; set; }

        public IList<PurchaseDataModel> PurchaseData { get; set; }
    }
}