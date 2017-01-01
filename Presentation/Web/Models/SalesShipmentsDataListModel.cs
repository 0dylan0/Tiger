using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class SalesShipmentsDataListModel
    {
        public string Name { get; set; }

        public IList<SalesShipmentsDataModel> SalesShipmentsData { get; set; }
    }
}