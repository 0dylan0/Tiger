using Core.Domain.Common;
using Dapper.FluentMap.Dommel.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class ArrearsDataMap : DommelEntityMap<ArrearsData>
    {
        public ArrearsDataMap()
        {
            ToTable("GoodsData");

            Map(t => t.ID).ToColumn("ID").IsKey();
            Map(t => t.ClientDataID).ToColumn("ClientData_ID");
            Map(t => t.ClientDataName).ToColumn("ClientData_Name");
            Map(t => t.ArrearsAmount).ToColumn("ArrearsAmount");
            Map(t => t.Date).ToColumn("Date"); 
            Map(t => t.Sum).ToColumn("Sum");

        }
    }
}
