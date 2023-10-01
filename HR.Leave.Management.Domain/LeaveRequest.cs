using HR.Leave.Management.Domain.Common;

namespace HR.Leave.Management.Domain
{
	public class LeaveRequest : BaseEntity
	{
		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public LeaveType? LeaveType { get; set; }

        public int LeaveTypeId { get; set; }

		public DateTime DateRequested { get; set; }

        public string? RequstComment { get; set; }

        public bool? Approved { get; set; }

        public bool Cancelled { get; set; }

		public string RequestingEmployeeId { get; set; } = string.Empty;
    }
}
