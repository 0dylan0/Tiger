using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Localization
{
    public class Language : BaseDicEntity
    {
        private ICollection<LocaleStringResource> _localeStringResources;

        public virtual ICollection<LocaleStringResource> LocaleStringResources
        {
            get
            {
                return _localeStringResources ?? (_localeStringResources = new List<LocaleStringResource>());
            }
            protected set
            {
                _localeStringResources = value;
            }
        }
    }
}
