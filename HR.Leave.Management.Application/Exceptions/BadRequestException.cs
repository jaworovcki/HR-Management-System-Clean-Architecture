using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Exceptions
{
	public class BadRequestException : Exception
	{
		public BadRequestException(string mesage):
			base(mesage)
		{

		}
	}
}
