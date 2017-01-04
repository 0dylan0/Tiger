using Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Web.Framework;

namespace Web.Models
{
    public class SearchInventoryDataModel
    {
        [DisplayName("商品名")]
        public string InventoryDataName { get; set; }

        public ChoiceType ChoiceTypes { get; set; }

        public string IdList { get; set; }

        public string NameList { get; set; }

        public IList<InventoryDataModel> InventoryDataList { get; set; }
    }
}