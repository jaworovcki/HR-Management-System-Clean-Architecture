using AutoMapper;
using HR.Leave.Management.Application.Contracts.Persistence;
using HR.Leave.Management.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation
{
	public class DeleteLeaveAllocationCommandHandler :
		IRequestHandler<DeleteLeaveAllocationCommand, Unit>
	{
		private readonly ILeaveAllocationRepository _leaveAllocationRepository;

		public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository)
        {
			_leaveAllocationRepository = leaveAllocationRepository;
		}
        public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
		{
			var validator = new DeleteLeaveAllocationCommandValidator(_leaveAllocationRepository);
			var validationResult = await validator.ValidateAsync(request);

			if (!validationResult.IsValid)
			{
				throw new NotFoundException("Invalid leave allocation id", validationResult);
			}

			var leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(request.Id);

			await _leaveAllocationRepository.DeleteAsync(leaveAllocation);

			return Unit.Value;
		}
	}
}
