using HR.Leave.Management.Application.Contracts.Persistence;
using HR.Leave.Management.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

#pragma warning disable 

namespace HR.LeaveManagement.Persistence.Repositories
{
	public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
	{
		public LeaveAllocationRepository(HrDataContext context) : base(context)
		{
		}

		public async Task<IEnumerable<LeaveAllocation>> AddAllocation(IEnumerable<LeaveAllocation> leaveAllocations)
		{
			_context.AddRange(leaveAllocations);

			await _context.SaveChangesAsync();

			return leaveAllocations;
		}

		public async Task<IEnumerable<LeaveAllocation>> GetLeaveAllocationsWithDetails() 
			=> await _context.LeaveAllocations
				.Include(x => x.LeaveType)
				.AsNoTracking()
				.ToListAsync();

		public async Task<IEnumerable<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId) 
			=> await _context.LeaveAllocations
				.Where(x => x.EmployeeId.Equals(userId))
				.Include(x => x.LeaveType)
				.AsNoTracking()
				.ToListAsync();

		public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
			=> await _context.LeaveAllocations
				.Include(x => x.LeaveType)
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.Id.Equals(id));

		public async Task<LeaveAllocation> GetUserLeaveAllocation(string userId, int leaveTypeId) 
			=> await _context.LeaveAllocations
				.FirstOrDefaultAsync(x => x.EmployeeId.Equals(userId) && x.LeaveTypeId.Equals(leaveTypeId));

		public Task<bool> IsAllocationExists(int leaveTypeId, string userId, int period) 
			=> _context.LeaveAllocations
				.AnyAsync(x => x.LeaveTypeId.Equals(leaveTypeId) && x.EmployeeId.Equals(userId) && x.Period.Equals(period));
	}
}
