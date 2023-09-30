using HR.Leave.Management.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveType.Commands.CreateLeaveType
{
	public class CreateLeaveTypeCommand : IRequest<LeaveTypeDto>
	{
		public string Name { get; set; } = string.Empty;

		public int DefaultDays { get; set; }
	}
}
