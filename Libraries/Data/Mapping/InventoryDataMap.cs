using Core.Domain;
using Dapper.FluentMap.Dommel.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class InventoryDataMap : DommelEntityMap<InventoryData>
    {
        public InventoryDataMap()
        {
            ToTable("PurchaseData");

            Map(t => t.ID).ToColumn("ID").IsKey();
            Map(t => t.GoodsID).ToColumn("Goods_ID");
            Map(t => t.GoodsName).ToColumn("Goods_Name");
            Map(t => t.Unit).ToColumn("Unit");

            Map(t => t.Specification).ToColumn("Specification");
            Map(t => t.GoodsType).ToColumn("GoodsType");
            Map(t => t.Brand).ToColumn("Brand");
            Map(t => t.InventoryQuantity).ToColumn("InventoryQuantity");

            Map(t => t.CostPrice).ToColumn("CostPrice");
            Map(t => t.InventorySum).ToColumn("InventorySum");
            Map(t => t.LastInventoryDate).ToColumn("LastInventoryDate");
            Map(t => t.FinalSaleDate).ToColumn("FinalSaleDate");

            Map(t => t.WarehouseID).ToColumn("Warehouse_ID");
            Map(t => t.WarehouseName).ToColumn("Warehouse_Name");
            Map(t => t.PurchaseDate).ToColumn("PurchaseDate");
            Map(t => t.ShipmentsDate).ToColumn("ShipmentsDate");

            Map(t => t.SupplierID).ToColumn("Supplier_ID");
            Map(t => t.SupplierName).ToColumn("Supplier_Name");
            Map(t => t.SupplierAddress).ToColumn("Supplier_Address");

            Map(t => t.Active).ToColumn("Active");
            Map(t => t.ShipmentsQuantity).ToColumn("ShipmentsQuantity");
            Map(t => t.RemainingQuantity).ToColumn("RemainingQuantity");
        }
    }
}
