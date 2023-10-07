using HR.Leave.Management.Application.Contracts.Persistence;
using HR.Leave.Management.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

#pragma warning disable
 
namespace HR.LeaveManagement.Persistence.Repositories
{
	public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
	{
		public LeaveRequestRepository(HrDataContext context) : base(context)
		{
		}

		public async Task<IEnumerable<LeaveRequest>> GetLeaveRequestsWithDetailsAsync()
		{
			var leaveRequests = await _context.LeaveRequests
				.Include(x => x.LeaveType)
				.AsNoTracking()
				.ToListAsync();

			return leaveRequests;
		}

		public async Task<IEnumerable<LeaveRequest>> GetLeaveRequestsWithDetaisAsync(string userId)
		{
			var leaveRequests = await _context.LeaveRequests
				.Where(x => x.RequestingEmployeeId.Equals(userId))
				.Include(x => x.LeaveType)
				.AsNoTracking()
				.ToListAsync();

			return leaveRequests;
		}

		public async Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(int id)
		{
			var leaveRequest = await _context.LeaveRequests
				.Include(x => x.LeaveType)
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id.Equals(id));

			return leaveRequest;
		}
	}
}
