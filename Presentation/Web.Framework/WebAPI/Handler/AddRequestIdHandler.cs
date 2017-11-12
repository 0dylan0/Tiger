using System;
using Core;
using Core.Infrastructure;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Web.Framework.WebAPI.Handler
{
    public class AddRequestIdHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            var workContext = request.GetDependencyScope().GetService(typeof(IWorkContext)) as IWorkContext;

            response.Headers.Add("X-KL-Request-Id", 
                //workContext.CurrentRequestId.ToString()
                Guid.NewGuid().ToString()
                );

            return response;
        }
    }
}
