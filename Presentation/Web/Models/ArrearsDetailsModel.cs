using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ArrearsDetailsModel
    {
        public int ID { get; set; }

        [DisplayName("商品ID")]
        public int GoodsID { get; set; }

        [DisplayName("商品名字")]
        public string GoodsName { get; set; }

        [DisplayName("欠款主表ID")]
        public int ArrearsID { get; set; }

        [DisplayName("销售出货ID")]
        public int SalesShipmentsDataID { get; set; }

        [DisplayName("数量")]
        public int Quantity { get; set; }

        [DisplayName("单价")]
        public decimal UnitPrice { get; set; }

        [DisplayName("总金额")]
        //（初始化后不要改变）
        public decimal Sum { get; set; }

        [DisplayName("欠款金额")]
        public decimal ArrearsAmount { get; set; }
    }
}