using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class InventoryDataListModel
    {
        public string Name { get; set; }

        public IList<InventoryDataModel> InventoryData { get; set; }
    }
}