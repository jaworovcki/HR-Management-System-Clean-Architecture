using HR.Leave.Management.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Domain
{
	public class LeaveType : BaseEntity
	{
		public string Name { get; set; } = string.Empty;

        public int DefaultDays { get; set; }
	}
}