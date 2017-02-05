using Core.Domain.Common;
using Dapper.FluentMap.Dommel.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class ArrearsDetailsMap : DommelEntityMap<ArrearsDetails>
    {
        public ArrearsDetailsMap()
        {
            ToTable("GoodsData");

            Map(t => t.ID).ToColumn("ID").IsKey();
            Map(t => t.GoodsID).ToColumn("Goodes_ID");
            Map(t => t.GoodsName).ToColumn("Goods_Name");
           
            Map(t => t.ArrearsID).ToColumn("Arrears_ID");

            Map(t => t.SalesShipmentsDataID).ToColumn("SalesShipmentsData_ID");
            Map(t => t.Quantity).ToColumn("Quantity");

            Map(t => t.UnitPrice).ToColumn("UnitPrice");
            Map(t => t.Sum).ToColumn("Sum");
            Map(t => t.ArrearsAmount).ToColumn("ArrearsAmount");
        }
    }
}
