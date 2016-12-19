using Core.Domain.Common;
using Dapper.FluentMap.Dommel.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class GoodsDataMap : DommelEntityMap<GoodsData>
    {
        public GoodsDataMap()
        {
            ToTable("GoodsData");

            Map(t => t.ID).ToColumn("ID").IsKey();
            Map(t => t.GoodsName).ToColumn("GoodsName");
            Map(t => t.Unit).ToColumn("Unit");
            Map(t => t.Brand).ToColumn("Brand");
            Map(t => t.GoodType).ToColumn("GoodType");

            Map(t => t.DefaultPurchasePrice).ToColumn("DefaultPurchasePrice");
            Map(t => t.ActualPurchasePrice).ToColumn("ActualPurchasePrice");
            Map(t => t.Inventory).ToColumn("Inventory");
            Map(t => t.Price1).ToColumn("Price1");

            Map(t => t.Price2).ToColumn("Price2");
            Map(t => t.Price3).ToColumn("Price3");
            Map(t => t.Price4).ToColumn("Price4");
            Map(t => t.Warehouse).ToColumn("Warehouse");

            Map(t => t.Cost).ToColumn("Cost");
            Map(t => t.Image).ToColumn("Image");
            Map(t => t.SingleProfit).ToColumn("SingleProfit");

        }
    }
}
