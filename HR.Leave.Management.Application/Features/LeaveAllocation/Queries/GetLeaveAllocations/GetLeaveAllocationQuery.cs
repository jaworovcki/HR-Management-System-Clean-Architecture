﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations
{
	public class GetLeaveAllocationQuery : IRequest<IEnumerable<LeaveAllocationDto>>
	{

	}
}
