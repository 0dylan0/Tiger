using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Web.Framework.Json
{
    /// <summary>
    /// 表示一个类，该类用于将 JSON 格式的内容发送到响应（序列化时使用Json.Net）。
    /// </summary>
    public class JsonResponseResult : JsonResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            if (JsonRequestBehavior == JsonRequestBehavior.DenyGet &&
                String.Equals(context.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("JsonRequest GetNotAllowed");
            }

            HttpResponseBase response = context.HttpContext.Response;

            if (!String.IsNullOrEmpty(ContentType))
            {
                response.ContentType = ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            if (Data != null)
            {
                // 使用 Json.Net 替代微软的 JavaScriptSerialization 进行序列化
                // 以确保返回的 JSON 属性名符合驼峰命名法
                var jsonContent = JsonConvert.SerializeObject(Data, JsonHelper.CommonSettings);

                response.Write(jsonContent);
            }
        }
    }
}

