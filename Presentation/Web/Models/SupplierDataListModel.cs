using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class SupplierDataListModel
    {
        public string Name { get; set; }

        public IList<SupplierDataModel> SupplierData { get; set; }
    }
}