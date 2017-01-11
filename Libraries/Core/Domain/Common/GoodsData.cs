using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Common
{
    public class GoodsData
    {
        public int ID { get; set; }

        //商品名称
        public string GoodsName { get; set; }

        //供应商名称
        //public string SuppireDataName { get; set; }

        //基本单位
        public string Unit { get; set; }

        //品牌
        public string Brand { get; set; }

        //商品类别
        public string GoodType { get; set; }

        //预计进价
        public decimal DefaultPurchasePrice { get; set; }


        //实际售价
        public decimal ActualPurchasePrice { get; set; }

        //库存
        public int Inventory { get; set; }

        //售价1
        public decimal Price1 { get; set; }

        //售价2
        public decimal Price2 { get; set; }
        //售价3
        public decimal Price3 { get; set; }

        //售价4
        public decimal Price4 { get; set; }

        //仓库
        public string Warehouse { get; set; }

        //成本
        public decimal Cost { get; set; }

        //图片
        public string Image { get; set; }

        //单品利润
        public decimal SingleProfit { get; set; }
    }
}
