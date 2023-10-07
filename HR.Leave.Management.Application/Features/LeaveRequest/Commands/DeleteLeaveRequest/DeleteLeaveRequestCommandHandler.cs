using HR.Leave.Management.Application.Contracts.Persistence;
using HR.Leave.Management.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest
{
	public class DeleteLeaveRequestCommandHandler : 
		IRequestHandler<DeleteLeaveRequestCommand, Unit>
	{
		private readonly ILeaveRequestRepository _leaveRequestRepository;

		public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository)
        {
			_leaveRequestRepository = leaveRequestRepository;
		}

        public async Task<Unit> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
		{
			var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

			if (leaveRequest is null)
			{
				throw new NotFoundException("Leave request not found", nameof(request.Id));
			}

			await _leaveRequestRepository.DeleteAsync(leaveRequest);

			return Unit.Value;
		}
	}
}
