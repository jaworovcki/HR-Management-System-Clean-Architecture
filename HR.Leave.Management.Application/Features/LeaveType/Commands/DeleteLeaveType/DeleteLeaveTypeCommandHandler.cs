using AutoMapper;
using HR.Leave.Management.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveType.Commands.DeleteLeaveType
{
	internal class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
	{
		private readonly ILeaveTypeRepository _leaveTypeRepository;

		public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository) 
			=> _leaveTypeRepository = leaveTypeRepository;

		public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
		{
			var leaveTypeDomain = await _leaveTypeRepository.GetByIdAsync(request.Id);

			await _leaveTypeRepository.DeleteAsync(leaveTypeDomain.Id);

			return Unit.Value;
		}
	}
}
