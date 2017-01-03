using Web.Framework.Json;
using Web.Framework.Security;
using Web.Framework.UI;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Mvc;

namespace Web.Framework.Controllers
{
    [Authorize]   
    //  [LicenseCheck]
    [IMAntiForgeryAttribute]
    public abstract class BaseController : Controller
    {
        #region PartialViewToString
        public virtual string RenderPartialViewToString()
        {
            return RenderPartialViewToString(null, null);
        }

        public virtual string RenderPartialViewToString(string viewName)
        {
            return RenderPartialViewToString(viewName, null);
        }

        public virtual string RenderPartialViewToString(object model)
        {
            return RenderPartialViewToString(null, model);
        }

        public virtual string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                ViewEngineResult viewResult = System.Web.Mvc.ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
        #endregion

        #region 页面跳转公共方法
        protected internal ActionResult AccessDenied()
        {
            return View("AccessDenied");
        }

        protected internal ActionResult RedirectToList()
        {
            return RedirectToAction("List");
        }

        protected internal ActionResult RedirectToLogin()
        {
            return Redirect("~/Login");
        }

        /// <summary>
        /// 使用路由值重定向到List页面的操作
        /// </summary>
        /// <param name="routeValues">路由的参数</param>
        /// <returns></returns>
        protected internal ActionResult RedirectToList(object routeValues)
        {
            return RedirectToAction("List", routeValues);
        }
        #endregion

        #region Json

        /// <summary>
        /// 创建一个将普通的 Json 返回对象中的 Content 属性序列化为 JSON 的 JsonResponseResult 对象
        /// </summary>
        /// <param name="data">普通的 Json 返回对象中的 Content 属性会被序列化为 Json</param>
        /// <param name="behavior">JSON 请求行为</param>
        /// <returns></returns>
        protected internal JsonResponseResult Json(PlainJsonResponse data, JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet)
        {
            return Json(data.Content, null /* contentType */, null /* contentEncoding */, behavior);
        }

        /// <summary>
        /// 创建一个将指定对象序列化为 JavaScript 对象表示法 (JSON) 的 JsonResponseResult 对象
        /// </summary>
        /// <param name="data">要序列化的 JavaScript 对象图</param>
        /// <param name="behavior">JSON 请求行为</param>
        /// <returns>将指定对象序列化为 JSON 格式的结果对象</returns>
        protected internal JsonResponseResult Json(JsonResponse data, JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet)
        {
            return Json(data, null /* contentType */, null /* contentEncoding */, behavior);
        }

        /// <summary>
        /// 创建一个将指定对象序列化为 JavaScript 对象表示法 (JSON) 的 JsonResponseResult 对象
        /// </summary>
        /// <param name="data">要序列化的 JavaScript 对象图</param>
        /// <param name="contentType">内容类型（MIME 类型）</param>
        /// <param name="contentEncoding">内容编码</param>
        /// <param name="behavior">JSON 请求行为</param>
        /// <returns>将指定对象序列化为 JSON 格式的结果对象</returns>
        protected internal JsonResponseResult Json(JsonResponse data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return Json<JsonResponse>(data, contentType, contentEncoding, behavior);
        }
        #endregion

        #region Json<T>

        /// <summary>
        /// 创建一个将指定对象序列化为 JavaScript 对象表示法 (JSON) 的 JsonResponseResult 对象
        /// </summary>
        /// <typeparam name="TContent">content 节点的内容的类型</typeparam>
        /// <param name="data">要序列化的 JavaScript 对象图</param>
        /// <param name="behavior">JSON 请求行为</param>
        /// <returns>将指定对象序列化为 JSON 格式的结果对象</returns>
        protected internal JsonResponseResult Json<TContent>(JsonResponse<TContent> data, JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet)
        {
            return Json(data, null /* contentType */, null /* contentEncoding */, behavior);
        }

        /// <summary>
        /// 创建一个将指定对象序列化为 JavaScript 对象表示法 (JSON) 的 JsonResponseResult 对象
        /// </summary>
        /// <typeparam name="TContent">content 节点的内容的类型</typeparam>
        /// <param name="data">要序列化的 JavaScript 对象图</param>
        /// <param name="contentType">内容类型（MIME 类型）</param>
        /// <param name="contentEncoding">内容编码</param>
        /// <param name="behavior">JSON 请求行为</param>
        /// <returns>将指定对象序列化为 JSON 格式的结果对象</returns>
        protected internal JsonResponseResult Json<TContent>(JsonResponse<TContent> data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return Json<JsonResponse<TContent>>(data, contentType, contentEncoding, behavior);
        }

        private JsonResponseResult Json<TContent>(TContent data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResponseResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior
            };
        }
        #endregion

        #region Notification
        protected void SuccessNotification(string message, bool persistForTheNextRequest = true)
        {
            AddNotification(NotifyType.Success, message, persistForTheNextRequest);
        }

        protected void ErrorNotification(string message, bool persistForTheNextRequest = true)
        {
            AddNotification(NotifyType.Error, message, persistForTheNextRequest);
        }

        protected void AddNotification(NotifyType type, string message, bool persistForTheNextRequest)
        {
            string dataKey = string.Format("kunlun.notifications.{0}", type);
            if (persistForTheNextRequest)
            {
                if (TempData[dataKey] == null)
                {
                    TempData[dataKey] = new List<string>();
                }
                ((List<string>)TempData[dataKey]).Add(message);
            }
            else
            {
                if (ViewData[dataKey] == null)
                {
                    ViewData[dataKey] = new List<string>();
                }
                ((List<string>)ViewData[dataKey]).Add(message);
            }
        }
        #endregion
    }
}