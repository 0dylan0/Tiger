using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Web.Framework.Extensions
{
    public static class HtmlHelperExtensions
    {
        #region HtmlHelper<TModel>

        /// <summary>
        /// 格式化DateTime为yyyy-MM-dd
        /// </summary>
        public static MvcHtmlString KLDateTextBoxFor<TModel, DateTime>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, DateTime>> expression, object htmlAttribute)
        {
            return htmlHelper.TextBoxFor(expression, "{0:yyyy-MM-dd}", htmlAttribute);
        }

        public static MvcHtmlString TimeDropdownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> hourExpression, int defaultHour, Expression<Func<TModel, TProperty>> minuteExpression, int defaultMinute, object htmlAttributes = null)
        {
            return TimeDropdownListFor(htmlHelper, hourExpression, defaultHour, htmlAttributes, minuteExpression, defaultMinute, htmlAttributes);
        }

        public static MvcHtmlString TimeDropdownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> hourExpression, int defaultHour, object hourAttributes, Expression<Func<TModel, TProperty>> minuteExpression, int defaultMinute, object minuteAttributes)
        {
            var hourSource = CreateDropdownListSource(0, 24, defaultHour);
            var minuteSource = CreateDropdownListSource(0, 60, defaultMinute);

            var hourElement = htmlHelper.DropDownListFor(hourExpression, hourSource, hourAttributes);
            var minuteElement = htmlHelper.DropDownListFor(minuteExpression, minuteSource, minuteAttributes);

            return MvcHtmlString.Create(hourElement.ToHtmlString() + " : " + minuteElement.ToHtmlString());
        }

        #endregion

        #region HtmlHelper

        /// <summary>
        /// 生成 type 为 text 的 input 元素，并且可以指定 id 是否与 name 相同。
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name">生成 input 元素的 name。</param>
        /// <param name="value">生成 input 元素的 value。</param>
        /// <param name="setIdSameAsName">设置一个值，当其为 true 时，生成 input 元素的 id 将会和 name 一致，若为 false 则按 MVC 默认规则生成。</param>
        /// <returns></returns>
        public static MvcHtmlString TextBox(this HtmlHelper htmlHelper, string name, object value, bool setIdSameAsName)
        {
            return htmlHelper.TextBox(name, value, setIdSameAsName, null);
        }

        /// <summary>
        /// 生成 type 为 text 的 input 元素，并且可以指定 id 是否与 name 相同。
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name">生成 input 元素的 name。</param>
        /// <param name="value">生成 input 元素的 value。</param>
        /// <param name="setIdSameAsName">设置一个值，当其为 true 时，生成 input 元素的 id 将会和 name 一致，若为 false 则按 MVC 默认规则生成。</param>
        /// <param name="htmlAttributes">一个对象，其中包含要为该元素设置的 HTML 特性。</param>
        /// <returns></returns>
        public static MvcHtmlString TextBox(this HtmlHelper htmlHelper, string name, object value, bool setIdSameAsName, object htmlAttributes)
        {
            return htmlHelper.TextBox(name, value, setIdSameAsName, null, htmlAttributes);
        }

        /// <summary>
        /// 生成 type 为 text 的 input 元素，并且可以指定 id 是否与 name 相同。
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="name">生成 input 元素的 name。</param>
        /// <param name="value">生成 input 元素的 value。</param>
        /// <param name="setIdSameAsName">设置一个值，当其为 true 时，生成 input 元素的 id 将会和 name 一致，若为 false 则按 MVC 默认规则生成。</param>
        /// <param name="formate">用于设置输入格式的字符串。</param>
        /// <param name="htmlAttributes">一个对象，其中包含要为该元素设置的 HTML 特性。</param>
        /// <returns></returns>
        public static MvcHtmlString TextBox(this HtmlHelper htmlHelper, string name, object value, bool setIdSameAsName, string formate, object htmlAttributes)
        {
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            if (setIdSameAsName)
            {
                attributes.Add("Id", name);
            }

            if (formate != null)
            {
                return htmlHelper.TextBox(name, value, formate, attributes);
            }

            return htmlHelper.TextBox(name, value, attributes);
        }

        #endregion

        #region Private

        private static IEnumerable<SelectListItem> CreateDropdownListSource(int start, int count, int? defaultValue = null)
        {
            foreach (var item in Enumerable.Range(start, count))
            {
                var itemString = item.ToString("00");
                yield return new SelectListItem { Text = itemString, Value = itemString, Selected = defaultValue.HasValue ? itemString.Equals(defaultValue.Value.ToString("00")) : false };
            }
        }

        #endregion
    }
}
