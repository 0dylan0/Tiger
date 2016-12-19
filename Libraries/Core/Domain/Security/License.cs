using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domain.Security
{
    public class License
    {
        public int Id { get; set; }

        public string HotelCode { get; set; }

        public string FunctionControl { get; set; }

        public string Module { get; set; }
    }
}
