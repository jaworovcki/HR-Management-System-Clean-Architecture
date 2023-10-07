using HR.Leave.Management.Domain;

namespace HR.Leave.Management.Application.Contracts.Persistence
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {
        Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(int id);

        Task<IEnumerable<LeaveRequest>> GetLeaveRequestsWithDetailsAsync();

        Task<IEnumerable<LeaveRequest>> GetLeaveRequestsWithDetaisAsync(string userId);
	}
}
