using Core.Domain;
using Core.Domain.Common;
using Dapper.FluentMap.Dommel.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class UserMap : DommelEntityMap<Core.Domain.Common.Users>
    {
        public UserMap()
        {
            ToTable("User");

            Map(t => t.ID).ToColumn("ID").IsKey();
            Map(t => t.Name).ToColumn("Name");
            Map(t => t.Password).ToColumn("Password");

        }
    }
}
