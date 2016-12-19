using Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Localization
{
    public class LocaleStringResource : BaseEntity
    {
        public int Id
        {
            get;
            set;
        }

        public string LanguageId
        {
            get;
            set;
        }

        public string ResourceName
        {
            get;
            set;
        }

        public string ResourceValue
        {
            get;
            set;
        }

        public virtual Language Language
        {
            get;
            set;
        }
    }
}
