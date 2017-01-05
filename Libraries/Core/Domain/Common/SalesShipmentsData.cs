using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Common
{
    public class SalesShipmentsData
    {
        public int ID { get; set; }

        //商品编号
        public int GoodsID { get; set; }

        //商品名称
        public string GoodsName { get; set; }

        //日期
        public DateTime Date { get; set; }

        //单位
        public string Unit { get; set; }

        //规格
        public string Specification { get; set; }

        //商品类别
        public string GoodsType { get; set; }

        //品牌
        public string Brand { get; set; }

        //数量
        public int Quantity { get; set; }

        //单价
        public string UnitPrice { get; set; }

        //成本
        public string Cost { get; set; }

        //利润
        public string Profit { get; set; }

        //金额
        public string Sum { get; set; }

        //总量
        public string Total { get; set; }

        //备注
        public string Remarks { get; set; }

        //供应商ID
        public int SupplierID { get; set; }

        //供应商名称
        public string SupplierName { get; set; }

        //供应商地址
        public string SupplierAddress { get; set; }

        public int WarehouseID { get; set; }

        public string WarehouseName { get; set; }
    }
}
