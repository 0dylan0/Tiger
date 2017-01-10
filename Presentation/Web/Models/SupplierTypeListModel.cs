using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class SupplierTypeListModel
    {
        [DisplayName("供应商类别名")]
        public string Name { get; set; }

        public IList<SupplierTypeModel> SupplierType { get; set; }
    }
}