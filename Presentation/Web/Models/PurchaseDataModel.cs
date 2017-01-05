using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Models
{
    public class PurchaseDataModel
    {
        public int ID { get; set; }

        [DisplayName("商品编号")]
        public int GoodsID { get; set; }

        public IEnumerable<SelectListItem> GoodsList { get; set; }

        [DisplayName("商品名称")]
        public string GoodsName { get; set; }

        [DisplayName("日期")]
        public DateTime Date { get; set; }

        [DisplayName("单位")]
        public string Unit { get; set; }

        [DisplayName("规格")]
        public string Specification { get; set; }

        [DisplayName("商品类别")]
        public string GoodsType { get; set; }

        [DisplayName("品牌")]
        public string Brand { get; set; }

        [DisplayName("数量")]
        public int Quantity { get; set; }

        [DisplayName("单价")]
        public decimal UnitPrice { get; set; }

        [DisplayName("金额")]
        public decimal Sum { get; set; }

        [DisplayName("总量")]
        public string Total { get; set; }

        [DisplayName("备注")]
        public string Remarks { get; set; }

        [DisplayName("供应商编号")]
        public int SupplierID { get; set; }

        public IEnumerable<SelectListItem> SupplierList { get; set; }

        [DisplayName("供应商名称")]
        public string SupplierName { get; set; }

        [DisplayName("供应商地址")]
        public string SupplierAddress { get; set; }

        [DisplayName("仓库ID")]
        public int WarehouseID { get; set; }

        public IEnumerable<SelectListItem> WarehouseList { get; set; }

        [DisplayName("仓库名")]
        public string WarehouseName { get; set; }
    }
}