using Core.Domain.Common;
using Dapper.FluentMap.Dommel.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class TransferCargoDataMap : DommelEntityMap<TransferCargoData>
    {
        public TransferCargoDataMap()
        {
            ToTable("TransferCargoData");

            Map(t => t.ID).ToColumn("ID").IsKey();
            Map(t => t.GoodsID).ToColumn("Goods_ID");
            Map(t => t.GoodsName).ToColumn("Goods_Name");
            Map(t => t.SupplierID).ToColumn("Supplier_ID");
            Map(t => t.SupplierName).ToColumn("Supplier_Name");
            

            Map(t => t.OldWarehouseID).ToColumn("OldWarehouse_ID");
            Map(t => t.OldWarehouseName).ToColumn("OldWarehouse_Name");
            Map(t => t.OldQuantity).ToColumn("OldQuantity");

            Map(t => t.NewWarehouseID).ToColumn("NewWarehouse_ID");
            Map(t => t.NewWarehouseName).ToColumn("NewWarehouse_Name");
            Map(t => t.NewQuantity).ToColumn("NewQuantity");

            Map(t => t.Date).ToColumn("Date");
        }
    }
}
