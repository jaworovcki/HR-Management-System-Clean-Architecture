using HR.Leave.Management.Domain;

namespace HR.Leave.Management.Application.Contracts.Persistence
{
    public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
    {
        Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);

        Task<IEnumerable<LeaveAllocation>> GetLeaveAllocationsWithDetails();

        Task<IEnumerable<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId);

        Task<bool> IsAllocationExists(int leaveTypeId, string userId, int period);

        Task<IEnumerable<LeaveAllocation>> AddAllocation(IEnumerable<LeaveAllocation> leaveAllocations);

        Task<LeaveAllocation> GetUserLeaveAllocation(string userId, int leaveTypeId);
    }
}
