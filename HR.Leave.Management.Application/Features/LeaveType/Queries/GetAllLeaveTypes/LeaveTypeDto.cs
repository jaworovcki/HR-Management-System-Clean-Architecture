﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
	public class LeaveTypeDto
	{
		public string Name { get; set; } = string.Empty;

		public int DefaultDays { get; set; }
	}
}