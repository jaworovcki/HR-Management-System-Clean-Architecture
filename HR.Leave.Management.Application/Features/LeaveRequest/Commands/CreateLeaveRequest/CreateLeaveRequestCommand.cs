using HR.Leave.Management.Application.Features.LeaveRequest.Queries.GetLeaveRequests;
using HR.Leave.Management.Application.Features.LeaveRequest.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveRequest.Commands.CreateLeaveRequest
{
	public class CreateLeaveRequestCommand : BaseLeaveRequest, IRequest<LeaveRequestDto>
	{
		public string RequestComment { get; set; } = string.Empty;
    }
}
