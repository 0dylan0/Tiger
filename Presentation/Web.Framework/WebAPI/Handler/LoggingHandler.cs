using Core;
using log4net;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Web.Framework.WebAPI.Handler
{
    public class LoggingHandler: DelegatingHandler
    {
        private const string API_PATH_PREFIX = @"\/api\/v";

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // 因为 swagger 文档页面也会进入 DelegatingHandler，所以这里把 API 的请求过滤出来
            // API 的请求肯定会包含 /api/v 
            if (Regex.IsMatch(request.RequestUri.AbsolutePath, API_PATH_PREFIX, RegexOptions.IgnoreCase))
            {
                LogRequestAsync(request);

                var response = await base.SendAsync(request, cancellationToken);

                LogResponseAsync(request, response);

                return response;
            }
            else
            {
                return await base.SendAsync(request, cancellationToken);
            }
        }

        private async void LogRequestAsync(HttpRequestMessage request)
        {
            var sb = new StringBuilder($"[{request.Method}] {request.RequestUri} - Request");
            sb.AppendLine();

            sb.AppendLine("[Request Header]");
            sb.Append(GetHeaders(request.Headers));
            sb.AppendLine(GetHeaders(request.Content.Headers));

            sb.AppendLine("[Request Body]");
            sb.AppendLine(await request.Content.ReadAsStringAsync());

            var workContext = request.GetDependencyScope().GetService(typeof(IWorkContext)) as IWorkContext;
            var logger = LogManager.GetLogger(
                //workContext.CurrentRequestId.ToString()
                Guid.NewGuid().ToString()
                );
            logger.Info(sb);
        }

        private async void LogResponseAsync(HttpRequestMessage request, HttpResponseMessage response)
        {
            var sb = new StringBuilder($"[{request.Method}] {request.RequestUri} - Response");
            sb.AppendLine();

            sb.AppendLine($"[Response Status]");
            sb.AppendLine($"{(int)response.StatusCode} {response.StatusCode.ToString()}");
            sb.AppendLine();

            sb.AppendLine("[Response Body]");
            if (response.Content != null)
            {
                sb.AppendLine(await response.Content.ReadAsStringAsync());
            }

            var workContext = request.GetDependencyScope().GetService(typeof(IWorkContext)) as IWorkContext;
            var logger = LogManager.GetLogger(
                //workContext.CurrentRequestId.ToString()
                Guid.NewGuid().ToString()
                );
            logger.Info(sb);
        }

        private string GetHeaders(HttpHeaders headers)
        {
            var sb = new StringBuilder();

            headers.ToList().ForEach(i =>
            {
                sb.AppendLine($"{i.Key}: {String.Join("", i.Value)}");
            });

            return sb.ToString();
        }
    }
}


