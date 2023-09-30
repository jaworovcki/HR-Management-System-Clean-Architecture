using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveType.Commands.DeleteLeaveType
{
	public class DeleteLeaveTypeCommand : IRequest<Unit>
	{
        public DeleteLeaveTypeCommand(int id)
        {
            Id = id;
        }
        
        public int Id { get; }
	}
}
