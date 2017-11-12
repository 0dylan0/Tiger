using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Framework.WebAPI.Models
{
    public class ErrorResponse
    {
        public string Code { get; set; }

        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }
    }
}
