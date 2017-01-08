using Core.Domain.Common;
using Dapper.FluentMap.Dommel.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class PurchaseDataMap : DommelEntityMap<PurchaseData>
    {
        public  PurchaseDataMap()
        {
            ToTable("PurchaseData");

            Map(t => t.ID).ToColumn("ID").IsKey();
            Map(t => t.GoodsID).ToColumn("Goods_ID");
            Map(t => t.GoodsName).ToColumn("Goods_Name");
            Map(t => t.Date).ToColumn("Date");
            Map(t => t.Unit).ToColumn("Unit");

            Map(t => t.Specification).ToColumn("Specification");
            Map(t => t.GoodsType).ToColumn("GoodsType");
            Map(t => t.Brand).ToColumn("Brand");
            Map(t => t.Quantity).ToColumn("Quantity");

            Map(t => t.UnitPrice).ToColumn("UnitPrice");
            Map(t => t.Sum).ToColumn("Sum");
            Map(t => t.Total).ToColumn("Total");
            Map(t => t.Remarks).ToColumn("Remarks");

            Map(t => t.SupplierID).ToColumn("Supplier_ID");
            Map(t => t.SupplierName).ToColumn("Supplier_Name");
            Map(t => t.SupplierAddress).ToColumn("Supplier_Address");

            Map(t => t.WarehouseID).ToColumn("Warehouse_ID");
            Map(t => t.WarehouseName).ToColumn("Warehouse_Name");

            Map(t => t.InventoryDataID).ToColumn("InventoryData_ID");
            Map(t => t.Active).ToColumn("Active");
            Map(t => t.Freight).ToColumn("Freight");
            
        }
        
    }
}
