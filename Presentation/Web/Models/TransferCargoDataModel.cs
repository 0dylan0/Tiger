using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class TransferCargoDataModel
    {
        public int ID { get; set; }

        //商品ID
        public int GoodsID { get; set; }

        //商品名
        public string GoodsName { get; set; }


        //供应商ID
        public int SupplierID { get; set; }

        //供应商名
        public string SupplierName { get; set; }

        //旧的仓库ID
        public int OldWarehouseID { get; set; }

        //旧的仓库名
        public string OldWarehouseName { get; set; }

        //旧的仓库量
        public int OldQuantity { get; set; }

        //新的仓库ID
        public int NewWarehouseID { get; set; }

        //新的仓库名
        public string NewWarehouseName { get; set; }

        //新的仓库量
        public int NewQuantity { get; set; }

        //调货日期
        public DateTime Date { get; set; }
    }
}