using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class WarehouseListModel
    {
        public string Name { get; set; }

        public IList<WarehouseModel> Warehouse { get; set; }
    }
}