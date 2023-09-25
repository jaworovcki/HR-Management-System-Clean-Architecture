using HR.Leave.Management.Domain.Common;

namespace HR.Leave.Management.Domain
{
	public class LeaveAllocation : BaseEntity
	{
		public int NumeberOfDays { get; set; }

		public LeaveType? LeaveType { get; set; } 

        public int LeaveTypeID { get; set; }

        public int Period { get; set; }
    }
}
