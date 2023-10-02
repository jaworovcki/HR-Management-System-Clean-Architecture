using HR.Leave.Management.Application.Contracts.Persistence;
using HR.Leave.Management.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence.Repositories
{
	public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
	{
		public LeaveTypeRepository(HrDataContext context) : base(context)
		{
		}

		public Task<bool> IsLeaveTypeExists(int id)
		{
			return _context.LeaveTypes.AnyAsync(x => x.Id.Equals(id));
		}

		public async Task<bool> IsLeaveTypeUnique(string name)
		{
			bool result = await _context.LeaveTypes.AnyAsync(x => x.Name.Equals(name));

			return !result;
		}

		public async Task<bool> IsLeaveTypeUnique(string name, int id)
		{
			bool resut = await _context.LeaveTypes.Where(x => x.Id != id).AnyAsync(x => x.Name.Equals(name));

			return !resut;
		}
	}


}
