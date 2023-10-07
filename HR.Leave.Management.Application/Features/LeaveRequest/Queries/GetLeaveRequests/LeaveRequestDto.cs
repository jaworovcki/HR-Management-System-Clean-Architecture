using HR.Leave.Management.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveRequest.Queries.GetLeaveRequests
{
	public class LeaveRequestDto
	{
		public string RequestingEmployeeId { get; set; } = string.Empty;

		public LeaveTypeDto? LeaveType { get; set; }

		public DateTime DateRequested { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public bool? Approved { get; set; }
	}
}
