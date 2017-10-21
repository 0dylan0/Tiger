using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public class CouponException : ApplicationException
    {
        public CouponException() : base("An exception occurred in the Kunlun layer.") { }

        public CouponException(string message) : base(message) { }

        public CouponException(Exception innerException) : base(innerException.Message, innerException) { }

        public CouponException(string message, Exception innerException) : base(message, innerException) { }

        protected CouponException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
