using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Models
{
    public class InventoryDataModel
    {
        [DisplayName("库存编号")]
        public int ID { get; set; }

        [DisplayName("仓库编号")]
        public int WarehouseID { get; set; }

        [DisplayName("仓库名称")]
        public string WarehouseName { get; set; }

        public IEnumerable<SelectListItem> WarehouseList { get; set; }

        [DisplayName("商品编号")]
        public int GoodsID { get; set; }

        [DisplayName("商品名称")]
        public string GoodsName { get; set; }

        [DisplayName("进货日期")]
        public DateTime PurchaseDate { get; set; }

        [DisplayName("出货日期")]
        public DateTime ShipmentsDate { get; set; }

        [DisplayName("单位")]
        public string Unit { get; set; }

        [DisplayName("规格")]
        public string Specification { get; set; }

        [DisplayName("商品类别")]
        public string GoodsType { get; set; }

        [DisplayName("品牌")]
        public string Brand { get; set; }

        [DisplayName("库存数量")]
        public int InventoryQuantity { get; set; }

        [DisplayName("成本价")]
        public decimal CostPrice { get; set; }

        [DisplayName("库存金额")]
        public decimal InventorySum { get; set; }

        [DisplayName("上次盘点日期")]
        public DateTime LastInventoryDate { get; set; }

        [DisplayName("最后销售日期")]
        public DateTime FinalSaleDate { get; set; }

        [DisplayName("供应商编号")]
        public int SupplierID { get; set; }

        [DisplayName("供应商名称")]
        public string SupplierName { get; set; }

        [DisplayName("供应商地址")]
        public string SupplierAddress { get; set; }

        [DisplayName("有效性")]
        //有效为1.无效为0
        public string Active { get; set; }

        [DisplayName("出货数量")]
        public int ShipmentsQuantity { get; set; }

        [DisplayName("剩余数量")]
        public int RemainingQuantity { get; set; }
    }
}