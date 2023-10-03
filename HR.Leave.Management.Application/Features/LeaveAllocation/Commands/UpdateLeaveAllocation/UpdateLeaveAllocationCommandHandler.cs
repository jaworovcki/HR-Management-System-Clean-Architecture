using AutoMapper;
using HR.Leave.Management.Application.Contracts.Persistence;
using HR.Leave.Management.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
	public class UpdateLeaveAllocationCommandHandler :
		IRequestHandler<UpdateLeaveAllocationCommand, Unit>
	{
		private readonly IMapper _mapper;
		private readonly ILeaveTypeRepository _leaveTypeRepository;
		private readonly ILeaveAllocationRepository _leaveAllocationRepository;

		public UpdateLeaveAllocationCommandHandler(IMapper mapper,
			ILeaveTypeRepository leaveTypeRepository,
			ILeaveAllocationRepository leaveAllocationRepository)
        {
			_mapper = mapper;
			_leaveTypeRepository = leaveTypeRepository;
			_leaveAllocationRepository = leaveAllocationRepository;
		}

        public async Task<Unit> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
		{
			var validator = new UpdateLeaveAllocationCommandValidator(_leaveAllocationRepository,_leaveTypeRepository);
			var validationResult = await validator.ValidateAsync(request);

			if (!validationResult.IsValid)
			{
				throw new BadRequestException("Invalid leave allocation request", validationResult);
			}

			var leaveAllocation = await _leaveAllocationRepository.GetLeaveAllocationWithDetails(request.Id);

			_mapper.Map(request, leaveAllocation, typeof(UpdateLeaveAllocationCommand), typeof(Domain.LeaveAllocation));

			await _leaveAllocationRepository.UpdateAsync(leaveAllocation);

			return Unit.Value;
		}
	}
}
