using Core;
using Core.Enum;
using Core.Infrastructure;
using Services.Localization;
using Web.Framework.Localization;
using System;
using System.IO;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Web.Framework.ViewEngines.Razor
{
    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        private LocalizationService _localizationService;
        private Localizer _localizer;
        private IWorkContext _workContext;

        public Localizer T
        {
            get
            {
                if (_localizer == null)
                {
                    _localizer = (format, args) =>
                    {
                        var resFormat = _localizationService.GetResource(format);
                        return string.IsNullOrEmpty(resFormat) ? new LocalizedString(format) : new LocalizedString((args == null || args.Length == 0) ? resFormat : string.Format(resFormat, args));
                    };
                }
                return _localizer;
            }
        }

        public IWorkContext WorkContext
        {
            get
            {
                return _workContext;
            }
        }

        public static MvcHtmlString HiddenLabel(bool hasText = true)
        {
            var builder = new TagBuilder("label");
            if (hasText)
            {
                builder.SetInnerText("a");
            }
            builder.MergeAttribute("class", "invisible full-width");
            builder.ToString(TagRenderMode.EndTag);

            return MvcHtmlString.Create(builder.ToString());
        }

        public override void InitHelpers()
        {
            base.InitHelpers();
            _localizationService = EngineContext.Current.Resolve<LocalizationService>();
            _workContext = EngineContext.Current.Resolve<IWorkContext>();
        }

        public HelperResult RenderWrappedSection(string name, object wrapperHtmlAttributes)
        {
            Action<TextWriter> action = delegate (TextWriter tw)
            {
                var htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(wrapperHtmlAttributes);
                var tagBuilder = new TagBuilder("div");
                tagBuilder.MergeAttributes(htmlAttributes);

                var section = RenderSection(name, false);
                if (section != null)
                {
                    tw.Write(tagBuilder.ToString(TagRenderMode.StartTag));
                    section.WriteTo(tw);
                    tw.Write(tagBuilder.ToString(TagRenderMode.EndTag));
                }
            };
            return new HelperResult(action);
        }

        public HelperResult RenderSection(string sectionName, Func<object, HelperResult> defaultContent)
        {
            return IsSectionDefined(sectionName) ? RenderSection(sectionName) : defaultContent(new object());
        }

        public MvcHtmlString LoadingInfo(bool isHide = false, string id = null, string messageResourceName = "Loadingdata")
        {
            var message = new TagBuilder("p");
            message.AddCssClass("text-center");
            message.SetInnerText(_localizationService.GetResource(messageResourceName));

            var progressBar = new TagBuilder("div");
            progressBar.AddCssClass("progress-bar progress-bar-striped");
            progressBar.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(new
            {
                style = "width: 100%",
                role = "progressbar",
                aria_valuenow = "40",
                aria_valuemin = "0",
                aria_valuemax = "100"
            }));

            var div = new TagBuilder("div");
            div.AddCssClass("progress active progress-sm");
            div.InnerHtml = progressBar.ToString();

            var box = new TagBuilder("div");
            box.AddCssClass("col-md-8 col-md-offset-2 kl-modal-loading-info");
            box.InnerHtml = message.ToString() + div.ToString();
            if (isHide)
            {
                box.AddCssClass("hide");
            }

            if (!String.IsNullOrEmpty(id))
            {
                box.Attributes.Add("id", id);
            }

            return MvcHtmlString.Create(box.ToString());
        }

        public static string FormatNumber<T>(T value, NumberDisplayFormat displayFormat = NumberDisplayFormat.Numeric, string left = null, string right = null, int digits = 2) where T : struct, IFormattable
        {
            return CommonHelper.FormatNumber(value, displayFormat, left, right, digits);
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
            return CommonHelper.ShowPercent(divisor, dividend, placeholder, left, right, digits);
        }
        public static string ShowExcept(decimal divisor, decimal dividend, string placeholder = "--", string left = null, string right = null, int digits = 2)
        {
            return CommonHelper.ShowExcept(divisor, dividend, placeholder, left, right, digits);
        }
        public override string Layout
        {
            get
            {
                var layout = base.Layout;

                if (!string.IsNullOrEmpty(layout))
                {
                    var filename = Path.GetFileNameWithoutExtension(layout);
                    ViewEngineResult viewResult = System.Web.Mvc.ViewEngines.Engines.FindView(ViewContext.Controller.ControllerContext, filename, "");

                    if (viewResult.View is RazorView)
                    {
                        layout = (viewResult.View as RazorView).ViewPath;
                    }
                }

                return layout;
            }
            set
            {
                base.Layout = value;
            }
        }
    }

    public abstract class WebViewPage : WebViewPage<dynamic> { }
}