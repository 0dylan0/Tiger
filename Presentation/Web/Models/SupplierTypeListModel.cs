using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class SupplierTypeListModel
    {
        public string Name { get; set; }

        public IList<SupplierTypeModel> SupplierType { get; set; }
    }
}