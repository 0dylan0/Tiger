using FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Validators;

namespace Web.Models
{
    [Validator(typeof(SalesShipmentsDataValidator))]
    public class SalesShipmentsDataModel
    {
        public int ID { get; set; }

        [DisplayName("商品编号")]
        public int GoodsID { get; set; }

        [DisplayName("商品名称")]
        public string GoodsName { get; set; }

        [DisplayName("送货日期")]
        public DateTime Date { get; set; }

        [DisplayName("单位")]
        public string Unit { get; set; }

        [DisplayName("规格")]
        public string Specification { get; set; }
        public IEnumerable<SelectListItem> SpecificationList { get; set; }

        [DisplayName("商品类别")]
        public string GoodsType { get; set; }
        public IEnumerable<SelectListItem> GoodsTypeList { get; set; }

        [DisplayName("品牌")]
        public string Brand { get; set; }

        [DisplayName("数量")]
        public int Quantity { get; set; }

        public int OldQuantity { get; set; }

        [DisplayName("实际单价")]
        public decimal UnitPrice { get; set; }

        [DisplayName("成本")]
        public decimal Cost { get; set; }

        [DisplayName("总利润")]
        public decimal Profit { get; set; }

        [DisplayName("总金额")]
        public decimal Sum { get; set; }

        [DisplayName("总量")]
        public string Total { get; set; }

        [DisplayName("备注")]
        public string Remarks { get; set; }

        public int WarehouseID { get; set; }

        [DisplayName("仓库名称")]
        public string WarehouseName { get; set; }

        public IEnumerable<SelectListItem> ClientDataList { get; set; }

        public IEnumerable<SelectListItem> WarehouseList { get; set; }

        //库存ID
        public int InventoryDataID { get; set; }

        [DisplayName("有效性")]
        //有效为1，无效为0
        public string Active { get; set; }

        [DisplayName("运费")]
        public decimal Freight { get; set; }

        //客户ID
        public int ClientDataID { get; set; }

        [DisplayName("客户")]
        public string ClientDataName { get; set; }
    }
}