using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Framework.Json
{
    public class PlainJsonResponse
    {
        public object Content { get; private set; }

        public PlainJsonResponse(object content)
        {
            Content = content;
        }
    }

    public class JsonResponse
    {
        protected JsonResponseStatus _status;
        protected string _message;

        public string Status
        {
            get
            {
                return _status.ToString();
            }
        }

        public string Message
        {
            get
            {
                return _message;
            }
        }

        /// <summary>
        /// 创建一个JsonResponse对象，并设置state为success
        /// </summary>
        public JsonResponse()
            : this(JsonResponseStatus.success)
        {
        }

        public JsonResponse(JsonResponseStatus status)
            : this(status, null)
        {
        }

        public JsonResponse(JsonResponseStatus status, string message)
        {
            _status = status;
            _message = message;
        }
    }

    public class JsonResponse<TContent> : JsonResponse
    {
        private TContent _content;

        public TContent Content
        {
            get
            {
                return _content;
            }
        }

        public DateTime Timestamp
        {
            get
            {
                return DateTime.Now.ToUniversalTime();
            }
        }

        /// <summary>
        /// 设置Json所返回的内容，此时状态为：success
        /// </summary>
        /// <param name="content"></param>
        public JsonResponse(TContent content)
            : this(content, JsonResponseStatus.success)
        {
        }

        /// <summary>
        /// 设置Json所返回的内容
        /// </summary>
        /// <param name="content"></param>
        /// <param name="status">响应状态</param>
        /// <param name="message">状态消息</param>
        public JsonResponse(TContent content, JsonResponseStatus status, string message = null)
        {
            _content = content;
            _status = status;
            _message = message;
        }
    }
}

