using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class WarehouseListModel
    {
        [DisplayName("仓库名")]
        public string Name { get; set; }

        public IList<WarehouseModel> Warehouse { get; set; }
    }
}