using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Api.Data.Exception
{
    public class ApiException : System.Exception
    {
		public ApiException() : base() { }
		public ApiException(string message) : base(message) { }
		public ApiException(string message, System.Exception innerException) : base(message, innerException) { }
		protected ApiException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
