using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveType.Queries.GetAllLeaveTypeDetais
{
	public class GetLeaveTypeDetailsQuery : IRequest<LeaveTypeDetailsDto>
	{
		public GetLeaveTypeDetailsQuery(int id)
		{
			Id = id;
		}

		public int Id { get; }
	}
}
