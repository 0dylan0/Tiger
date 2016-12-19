using Core;
using Core.Infrastructure;
using Services.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Web.Framework.Extensions
{
    public static class EnumExtensions
    {
        public static SelectList ToSelectList<TEnum>(this TEnum enumObj,
                                                        bool markCurrentAsSelected = true,
                                                        IEnumerable<int> valuesToExclude = null,
                                                        IEnumerable<int> valuesToInclude = null) where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum) throw new ArgumentException("An Enumeration type is required.", "enumObj");

            var localizationService = EngineContext.Current.Resolve<LocalizationService>();
            var workContext = EngineContext.Current.Resolve<IWorkContext>();

            var values = from TEnum enumValue in Enum.GetValues(typeof(TEnum))
                         where valuesToInclude == null || valuesToInclude.Contains(Convert.ToInt32(enumValue))
                         where valuesToExclude == null || !valuesToExclude.Contains(Convert.ToInt32(enumValue))
                         select new
                         {
                             ID = Convert.ToInt32(enumValue),
                             Name = enumValue
                         };

            object selectedValue = null;
            if (markCurrentAsSelected)
            {
                selectedValue = Convert.ToInt32(enumObj);
            }

            return new SelectList(values, "ID", "Name", selectedValue);
        }
    }
}
