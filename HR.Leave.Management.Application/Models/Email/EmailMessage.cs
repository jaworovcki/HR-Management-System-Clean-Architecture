using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Models.Email
{
	public class EmailMessage
	{
		public string To { get; set; } = string.Empty;

		public string Subject { get; set; } = string.Empty;

		public string Body { get; set; } = string.Empty;
	}
}
