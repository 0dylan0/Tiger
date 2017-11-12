using Web.Framework.Extensions;
using Web.Framework.WebAPI.Models;
using Web.Framework.WebAPI.Page;
using Web.Framework.WebAPI.Result;
using System.Net;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Results;

namespace Web.Framework.WebAPI.Controllers
{
    public class BaseApiController : ApiController
    {
        protected internal NegotiatedContentResult<ErrorResponse> BadRequest(string code, string message)
        {
            return Content(HttpStatusCode.BadRequest, new ErrorResponse { Code = code, Message = message });
        }

        protected internal IHttpActionResult NoContent()
        {
            return new NoContentResult();
        }

        protected internal NegotiatedContentResult<ErrorResponse> NotFound(string code, string message)
        {
            return Content(HttpStatusCode.NotFound, new ErrorResponse { Code = code, Message = message });
        }

        protected internal NegotiatedContentResult<ErrorResponse> Conflict(string code, string message)
        {
            return Content(HttpStatusCode.Conflict, new ErrorResponse { Code = code, Message = message });
        }

        protected internal NegotiatedContentResult<ErrorResponse> InternalServerError(string code, string message)
        {
            return Content(HttpStatusCode.InternalServerError, new ErrorResponse { Code = code, Message = message });
        }

        protected internal NegotiatedContentResult<ValidationErrorResponse> UnprocessableEntity(ModelStateDictionary modelState)
        {
            return Content((HttpStatusCode)422, modelState.MapToErrorResponse());
        }

        protected internal NegotiatedContentResult<ErrorResponse> InsertConsumeHistoryOk(string code, string message)
        {
            return Content(HttpStatusCode.OK, new ErrorResponse { Code = code, Message = message });
        }

        protected internal NegotiatedContentResult<ErrorResponse> UpdateConsumeHistoryOk(string code, string message)
        {
            return Content(HttpStatusCode.OK, new ErrorResponse { Code = code, Message = message });
        }

        protected internal IHttpActionResult Ok<T>(PagingLinkBuilder<T> data)
        {
            return new PagingResult<T>(data, Request);
        }
    }
}