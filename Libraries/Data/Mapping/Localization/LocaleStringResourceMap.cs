using Core.Domain.Localization;
using Dapper.FluentMap.Dommel.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping.Localization
{
    public class LocaleStringResourceMap : DommelEntityMap<LocaleStringResource>
    {
        public LocaleStringResourceMap()
        {
            ToTable("LocaleStringResource");

            Map(p => p.Id).ToColumn("id").IsKey();
        }
    }
}
