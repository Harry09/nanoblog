using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Api.Data.Dto
{
    public class ErrorDto
    {
		public string Message { get; private set; }

		public ErrorDto(string Message)
		{
			this.Message = Message;
		}
	}
}
