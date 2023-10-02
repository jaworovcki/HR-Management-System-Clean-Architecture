using HR.Leave.Management.Domain;

namespace HR.Leave.Management.Application.Contracts.Persistence
{
    public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
    {
        Task<bool> IsLeaveTypeUnique(string name);

        Task<bool> IsLeaveTypeUnique(string name, int id);

		Task<bool> IsLeaveTypeExists(int id);
    }
}
