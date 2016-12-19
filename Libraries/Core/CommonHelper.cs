using Core.ComponentModel;
using Core.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Core
{
    public static class CommonHelper
    {
        /// <summary>
        /// SQL 查询中 LIKE 的通配符
        /// </summary>
        public const char LIKE_WILDCARDS = '%';

        public static string EnsureSubscriberEmailOrThrow(string email)
        {
            string output = EnsureNotNull(email);
            output = output.Trim();
            output = EnsureMaximumLength(output, 255);

            if (!IsValidEmail(output))
            {
                throw new IMException("Email is not valid.");
            }

            return output;
        }

        public static bool IsValidEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                return false;
            }

            email = email.Trim();
            var result = Regex.IsMatch(email, "^(?:[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+\\.)*[\\w\\!\\#\\$\\%\\&\\'\\*\\+\\-\\/\\=\\?\\^\\`\\{\\|\\}\\~]+@(?:(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!\\.)){0,61}[a-zA-Z0-9]?\\.)+[a-zA-Z0-9](?:[a-zA-Z0-9\\-](?!$)){0,61}[a-zA-Z0-9]?)|(?:\\[(?:(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\.){3}(?:[01]?\\d{1,2}|2[0-4]\\d|25[0-5])\\]))$", RegexOptions.IgnoreCase);
            return result;
        }

        public static bool IsNumber(string number)
        {
            if (String.IsNullOrEmpty(number))
            {
                return false;
            }

            return Regex.IsMatch(number, "^[0-9]*?$");
        }

        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            string str = string.Empty;
            for (int i = 0; i < length; i++)
            {
                str = String.Concat(str, random.Next(10).ToString());
            }
            return str;
        }

        public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }

        public static string EnsureMaximumLength(string str, int maxLength, string postfix = null)
        {
            if (String.IsNullOrEmpty(str))
            {
                return str;
            }
            if (str.Length > maxLength)
            {
                var result = str.Substring(0, maxLength);
                if (!String.IsNullOrEmpty(postfix))
                {
                    result += postfix;
                }
                return result;
            }

            return str;
        }

        public static string EnsureNumericOnly(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return string.Empty;
            }

            var result = new StringBuilder();
            foreach (char c in str)
            {
                if (Char.IsDigit(c))
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }

        public static string EnsureNotNull(string str)
        {
            return str ?? string.Empty;
        }

        public static bool AreNullOrEmpty(params string[] stringsToValidate)
        {
            bool result = false;
            Array.ForEach(stringsToValidate, str => { if (string.IsNullOrEmpty(str)) result = true; });
            return result;
        }

        public static string ConvertEnum(string str)
        {
            string result = string.Empty;
            char[] letters = str.ToCharArray();
            foreach (char c in letters)
            {
                if (c.ToString() != c.ToString().ToLower())
                {
                    result += " " + c;
                }
                else
                {
                    result += c.ToString();
                }
            }
            return result;
        }

        public static TypeConverter GetCustomTypeConverter(Type type)
        {
            if (type == typeof(List<int>))
            {
                return new GenericListTypeConverter<int>();
            }
            if (type == typeof(List<decimal>))
            {
                return new GenericListTypeConverter<decimal>();
            }
            if (type == typeof(List<string>))
            {
                return new GenericListTypeConverter<string>();
            }

            return TypeDescriptor.GetConverter(type);
        }

        public static string ToObjectString<T>(T t) where T : new()
        {
            string tStr = string.Empty;
            if (t == null)
            {
                return tStr;
            }

            if (t.GetType().IsArray)
            {

                int rank = (int)t.GetType().InvokeMember("Length", BindingFlags.GetProperty, Type.DefaultBinder, t, null);
                for (int i = 0; i < rank; i++)
                {
                    object value = t.GetType().InvokeMember("Get", BindingFlags.InvokeMethod, Type.DefaultBinder, t, new object[] { i });
                    tStr += ToObjectString(value);
                }
                return tStr;
            }

            PropertyInfo[] properties = t.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
            if (properties.Length <= 0)
            {
                return tStr;
            }
            foreach (PropertyInfo item in properties)
            {
                string name = item.Name;
                if (name.Equals("sign", StringComparison.InvariantCultureIgnoreCase) || name.EndsWith("Specified", StringComparison.InvariantCultureIgnoreCase))
                {
                    continue;
                }
                object value = item.GetValue(t, null);
                if (value == null)
                {
                    continue;
                }
                if (item.PropertyType.Name.StartsWith("String"))
                {
                    tStr += value;
                    continue;
                }
                if (item.PropertyType.IsValueType)
                {
                    //debug
                    //tStr += string.Format("{0}:{1},", name, value);

                    PropertyInfo itemSpecified = properties.SingleOrDefault(p => p.Name == name + "Specified");
                    if (itemSpecified != null && !(bool)itemSpecified.GetValue(t, null))
                    {
                        continue;
                    }

                    if (value.GetType() == typeof(DateTime))
                    {
                        value = ((DateTime)value).ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
                    }

                    tStr += value;
                }
                else
                {
                    tStr += ToObjectString(value);
                }
            }
            return tStr;
        }

        public static string TrimSpace(string oldStr)
        {
            return Regex.Replace(oldStr, @"\s+", "");
        }

        public static IEnumerable<DateTime> EachDate(DateTime startDate, DateTime endDate)
        {
            for (var day = startDate.Date; day <= endDate.Date; day = day.AddDays(1))
            {
                yield return day;
            }
        }

        public static IEnumerable<int> GetLastYear(int length = 5)
        {
            var currentYear = DateTime.Now.Year;
            for (int year = currentYear - length; year <= currentYear; year++)
            {
                yield return year;
            }
        }

        public static IEnumerable<int> GetMonths()
        {
            for (int month = 1; month <= 12; month++)
            {
                yield return month;
            }
        }
        public static List<int> InitDays(int year)
        {
            var dayList = new List<int>();
            for (var i = 1; i <= 12; i++)
            {
                dayList.Add(DateTime.DaysInMonth(year, i));
            }
            return dayList;
        }

        public static IEnumerable<int> GetYearList(int length = 5)
        {
            var currentYear = DateTime.Now.Year;
            for (int year = currentYear - length; year <= currentYear + 1; year++)
            {
                yield return year;
            }
        }


        public static Image GetImageFromBase64String(string base64String)
        {
            var bytes = Convert.FromBase64String(base64String);
            using (var ms = new MemoryStream(bytes))
            {
                return Image.FromStream(ms);
            }
        }

        public static Image GetImageFromDataUri(string dataUri)
        {
            // DataURI 格式：data:[<mediatype>][;base64],<data>
            var base64String = Regex.Replace(dataUri, "^data:.*(;base64)?,", String.Empty);

            return GetImageFromBase64String(base64String);
        }

        public static string FormatNumber<T>(T value, NumberDisplayFormat displayFormat = NumberDisplayFormat.Numeric, string left = null, string right = null, int digits = 2) where T : struct, IFormattable
        {
            string format;

            switch (displayFormat)
            {
                case NumberDisplayFormat.Numeric:
                    format = "n";
                    break;
                case NumberDisplayFormat.Percentage:
                    format = "p";
                    break;
                default:
                    throw new Exception("不支持");
            }

            return String.Format("{0}{1}{2}", left, value.ToString(format + digits, CultureInfo.InvariantCulture), right);
        }

        /// <summary>
        /// 计算值并显示为百分比。当分母为 0 时显示指定符号（默认为 “--”）
        /// </summary>
        /// <param name="divisor">除数</param>
        /// <param name="dividend">被除数</param>
        /// <param name="placeholder">被除数为0时显示的内容</param>
        /// <param name="left">左侧填充文字</param>
        /// <param name="right">右侧填充文字</param>
        /// <param name="digits">保留小数位数</param>
        /// <returns></returns>
        public static string ShowPercent(decimal divisor, decimal dividend, string placeholder = "--", string left = null, string right = null, int digits = 2)
        {
            var value = GetPercent(divisor, dividend);
            if (value.HasValue == false)
            {
                return placeholder;
            }

            return FormatNumber(value.Value, NumberDisplayFormat.Percentage, left, right, digits);
        }
        public static string ShowExcept(decimal divisor, decimal dividend, string placeholder = "--", string left = null, string right = null, int digits = 2)
        {
            var value = GetPercent(divisor, dividend);
            if (value.HasValue == false)
            {
                return placeholder;
            }

            return FormatNumber(value.Value, NumberDisplayFormat.Numeric, left, right, digits);
        }
        public static decimal? GetPercent(decimal divisor, decimal dividend)
        {
            if (dividend == 0)
            {
                return null;
            }

            return divisor / dividend;
        }
    }
}
