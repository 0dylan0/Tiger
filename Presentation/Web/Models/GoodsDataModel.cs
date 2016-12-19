using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class GoodsDataModel
    {
        public int ID { get; set; }

        [DisplayName("商品名称")]
        //商品名称
        public string GoodsName { get; set; }


        //供应商名称
        //public string SuppireDataName { get; set; }

        [DisplayName("基本单位")]
        //基本单位
        public string Unit { get; set; }

        [DisplayName("品牌")]
        //品牌
        public string Brand { get; set; }

        [DisplayName("商品类别")]
        //商品类别
        public string GoodType { get; set; }

        [DisplayName("预计进价")]
        //预计进价
        public decimal DefaultPurchasePrice { get; set; }

        [DisplayName("实际售价")]
        //实际售价
        public decimal ActualPurchasePrice { get; set; }

        [DisplayName("库存")]
        //库存
        public string Inventory { get; set; }

        [DisplayName("售价1")]
        //售价1
        public decimal Price1 { get; set; }

        [DisplayName("售价2")]
        //售价2
        public decimal Price2 { get; set; }

        [DisplayName("售价3")]
        //售价3
        public decimal Price3 { get; set; }

        [DisplayName("售价4")]
        //售价4
        public decimal Price4 { get; set; }

        [DisplayName("仓库")]
        //仓库
        public string Warehouse { get; set; }

        [DisplayName("成本")]
        //成本
        public string Cost { get; set; }

        [DisplayName("图片")]
        //图片
        public string Image { get; set; }

        [DisplayName("单品利润")]
        //单品利润
        public string SingleProfit { get; set; }
    }
}