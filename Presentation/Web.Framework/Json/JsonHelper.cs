using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Framework.Json
{
    public static class JsonHelper
    {
        public static JsonSerializerSettings CommonSettings
        {
            get
            {
                return new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,
                    DateTimeZoneHandling = DateTimeZoneHandling.Local,
                    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat,
                    ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
                };
            }
        }
    }
}
