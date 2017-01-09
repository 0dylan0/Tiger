using Core.Domain.Common;
using Dapper.FluentMap.Dommel.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class SalesShipmentsDataMap : DommelEntityMap<SalesShipmentsData>
    {
        public SalesShipmentsDataMap()
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
            Map(t => t.Cost).ToColumn("Cost");
            Map(t => t.Profit).ToColumn("Profit");
            Map(t => t.Sum).ToColumn("Sum");

            Map(t => t.Total).ToColumn("Total");
            Map(t => t.Remarks).ToColumn("Remarks");
            Map(t => t.WarehouseID).ToColumn("Warehouse_ID");
            Map(t => t.WarehouseName).ToColumn("Warehouse_Name");
        }
    }
}
