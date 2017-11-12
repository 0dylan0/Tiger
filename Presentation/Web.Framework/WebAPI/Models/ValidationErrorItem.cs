using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Framework.WebAPI.Models
{
    public class ValidationErrorItem
    {
        public string Field { get; set; }

        public string ErrorMessage { get; set; }
    }
}
