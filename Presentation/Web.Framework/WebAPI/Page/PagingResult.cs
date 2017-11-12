using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Web.Framework.WebAPI.Page
{
    public class PagingResult<T> : IHttpActionResult
    {
        private PagingLinkBuilder<T> _pagingLinkbuilder;
        private HttpRequestMessage _request;

        public PagingResult(PagingLinkBuilder<T> pagingLinkBuilder, HttpRequestMessage request)
        {
            _pagingLinkbuilder = pagingLinkBuilder;
            _request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = _request.CreateResponse(HttpStatusCode.OK, _pagingLinkbuilder.Content);

            response.Headers.Add("Link", String.Join(", ", _pagingLinkbuilder.Link));

            return Task.FromResult(response);
        }
    }
}
