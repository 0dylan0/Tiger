using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class IMException : ApplicationException
    {
        public IMException() : base("An exception occurred in the Kunlun layer.") { }

        public IMException(string message) : base(message) { }

        public IMException(Exception innerException) : base(innerException.Message, innerException) { }

        public IMException(string message, Exception innerException) : base(message, innerException) { }

        protected IMException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
