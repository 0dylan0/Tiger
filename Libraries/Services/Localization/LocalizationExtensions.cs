using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Localization
{
    public static class LocalizationExtensions
    {
        //public static string GetLocalizedEnum<T>(this T enumValue, LocalizationService localizationService, IWorkContext workContext) where T : struct
        //{
        //    return GetLocalizedEnum(enumValue, localizationService, workContext.CurrentLanguage.Code);
        //}

        public static string GetLocalizedEnum<T>(this T enumValue, LocalizationService localizationService, string languageId) where T : struct
        {
            string resourceName = string.Format("Enums.{0}.{1}", typeof(T), enumValue);
            string result = localizationService.GetResource(resourceName, languageId, "", true);

            if (String.IsNullOrEmpty(result))
            {
                result = CommonHelper.ConvertEnum(enumValue.ToString());
            }
            return result;
        }
    }
}
