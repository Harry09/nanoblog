using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nanoblog.Common.Dto
{
    public class ErrorDto
    {
		public string Message { get; private set; }

		public ErrorDto(string message)
		{
			this.Message = message;
		}
	}
}
