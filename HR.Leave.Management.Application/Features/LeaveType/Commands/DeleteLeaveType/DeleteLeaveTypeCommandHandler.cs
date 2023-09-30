using AutoMapper;
using HR.Leave.Management.Application.Contracts.Persistence;
using HR.Leave.Management.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveType.Commands.DeleteLeaveType
{
	public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
	{
		private readonly ILeaveTypeRepository _leaveTypeRepository;

		public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository) 
			=> _leaveTypeRepository = leaveTypeRepository;

		public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
		{
			var leaveTypeDomain = await _leaveTypeRepository.GetByIdAsync(request.Id);

			if (leaveTypeDomain is null)
				throw new NotFoundException(nameof(leaveTypeDomain), request.Id);

			await _leaveTypeRepository.DeleteAsync(leaveTypeDomain.Id);

			return Unit.Value;
		}
	}
}
