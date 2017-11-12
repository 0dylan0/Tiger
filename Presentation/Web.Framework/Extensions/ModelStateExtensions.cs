using Web.Framework.WebAPI.Models;
using System;
using System.Linq;
using System.Web.Http.ModelBinding;

namespace Web.Framework.Extensions
{
    public static class ModelStateExtensions
    {
        public static ValidationErrorResponse MapToErrorResponse(this ModelStateDictionary modelState)
        {
            var errors = modelState.Where(ms => ms.Value.Errors.Count > 0)
                .SelectMany(ms => ms.Value.Errors.Select(v => new ValidationErrorItem
                {
                    Field = ms.Key,
                    ErrorMessage = GetErrorMessage(v)
                })).ToList();

            return new ValidationErrorResponse(errors);
        }

        private static string GetErrorMessage(ModelError modelError)
        {
            var errorMessage = modelError.ErrorMessage;

            if (modelError.Exception != null)
            {
                var exceptionMessage = modelError.Exception.Message;

                if (String.IsNullOrEmpty(errorMessage))
                {
                    errorMessage = exceptionMessage;
                }
                else
                {
                    errorMessage += Environment.NewLine + exceptionMessage;
                }
            }

            return errorMessage;
        }
    }
}

