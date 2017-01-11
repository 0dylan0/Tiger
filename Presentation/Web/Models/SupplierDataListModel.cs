using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class SupplierDataListModel
    {
        [DisplayName("供应商名")]
        public string Name { get; set; }

        public IList<SupplierDataModel> SupplierData { get; set; }
    }
}