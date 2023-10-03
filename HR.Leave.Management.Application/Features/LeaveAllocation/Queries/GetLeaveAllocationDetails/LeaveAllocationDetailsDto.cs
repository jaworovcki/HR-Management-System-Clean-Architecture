﻿using HR.Leave.Management.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails
{
	public class LeaveAllocationDetailsDto
	{
		public int Id { get; set; }

		public int NumeberOfDays { get; set; }

		public LeaveTypeDto? LeaveType { get; set; }

		public int LeaveTypeId { get; set; }

		public int Period { get; set; }

		public DateTime? DateCreated { get; set; }

		public DateTime? DateModified { get; set; }
	}
}
