using HR.Leave.Management.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommand : IRequest<LeaveAllocationDto>
    {
        public int LeaveTypeId { get; set; }
    }
}
