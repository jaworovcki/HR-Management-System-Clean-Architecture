using HR.Leave.Management.Domain;

namespace HR.Leave.Management.Application.Contracts.Persistence
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {
        Task<LeaveRequest> GetLeaveRequestWithDetails(int id);

        Task<IEnumerable<LeaveRequest>> GetLeaveRequestsWithDetails();

        Task<IEnumerable<LeaveRequest>> GetLeaveRequestsWithDetais(string userId);
    }
}
