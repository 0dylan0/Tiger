using Core.Domain.Common;
using Dapper.FluentMap.Dommel.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class WarehouseMap : DommelEntityMap<Warehouse>
    {
        public WarehouseMap()
        {
            ToTable("User");

            Map(t => t.ID).ToColumn("ID").IsKey();
            Map(t => t.Name).ToColumn("Name");
        }
    }
}
