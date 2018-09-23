using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Api.Data.Dto
{
    public class ExceptionDto
    {
		public string Message { get; private set; }

		public ExceptionDto(string Message)
		{
			this.Message = Message;
		}
	}
}
