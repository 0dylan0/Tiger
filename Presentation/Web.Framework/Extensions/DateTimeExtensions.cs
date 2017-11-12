using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Framework.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToKLDateString(this DateTime datetime)
        {
            return datetime.ToString("yyyy-MM-dd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }

        public static string ToKLTimeString(this DateTime datetime)
        {
            return datetime.ToString("HH:mm", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }

        public static string ToKLDateTimeString(this DateTime datetime)
        {
            return datetime.ToString("yyyy-MM-dd HH:mm", System.Globalization.DateTimeFormatInfo.InvariantInfo);
        }
    }
}
