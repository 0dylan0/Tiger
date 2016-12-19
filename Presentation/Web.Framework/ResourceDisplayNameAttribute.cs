using Core;
using Core.Infrastructure;
using Services.Localization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Framework
{
    public class ResourceDisplayNameAttribute : DisplayNameAttribute
    {
        private string _resourceValue = string.Empty;

        public ResourceDisplayNameAttribute(string resourceKey) : base(resourceKey)
        {
            ResourceKey = resourceKey;
        }

        public string ResourceKey
        {
            get;
            set;
        }

        public override string DisplayName
        {
            get
            {
                //var langId = EngineContext.Current.Resolve<IWorkContext>().CurrentLanguage.Code;
                //_resourceValue = EngineContext.Current.Resolve<LocalizationService>().GetResource(ResourceKey, langId, ResourceKey);
                return _resourceValue;
            }
        }

        public string Name
        {
            get
            {
                return "ResourceDisplayName";
            }
        }
    }
}
