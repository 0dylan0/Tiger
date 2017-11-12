using System.Collections.Generic;
using System.Linq;

namespace Web.Framework.WebAPI.Models
{
    public class ValidationErrorResponse
    {
        public ValidationErrorResponse()
        {

        }

        public ValidationErrorResponse(IEnumerable<ValidationErrorItem> items)
        {
            Errors = items.ToList();
        }

        public List<ValidationErrorItem> Errors { get; set; }
    }
}
