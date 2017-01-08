using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain
{
    public class InventoryData
    {
        public int ID { get; set; }

        //仓库编号
        public int WarehouseID { get; set; }

        //仓库名称
        public string WarehouseName { get; set; }

        //商品编号
        public int GoodsID { get; set; }

        //商品名称
        public string GoodsName { get; set; }

        //进货日期
        public DateTime PurchaseDate { get; set; }

        //出货日期
        public DateTime ShipmentsDate { get; set; }

        //单位
        public string Unit { get; set; }

        //规格
        public string Specification { get; set; }

        //商品类别
        public string GoodsType { get; set; }

        //品牌
        public string Brand { get; set; }

        //库存数量
        public int InventoryQuantity { get; set; }

        //成本
        public decimal CostPrice { get; set; }

        //库存金额
        public decimal InventorySum { get; set; }

        //上次盘点日期
        public DateTime LastInventoryDate { get; set; }

        //最后销售日期
        public DateTime FinalSaleDate { get; set; }

        //供应商编号
        public int SupplierID { get; set; }

        //供应商名称
        public string SupplierName { get; set; }

        //供应商地址
        public string SupplierAddress { get; set; }

        //有效性
        public string Active { get; set; }

        //出货数量
        public int ShipmentsQuantity { get; set; }

        //剩余数量
        public int RemainingQuantity { get; set; }


    }
}
