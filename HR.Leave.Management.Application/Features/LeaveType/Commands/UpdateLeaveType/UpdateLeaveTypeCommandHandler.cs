using AutoMapper;
using HR.Leave.Management.Application.Contracts.Persistence;
using HR.Leave.Management.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveType.Commands.UpdateLeaveType
{
	public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
	{
		private readonly IMapper _mapper;
		private readonly ILeaveTypeRepository _leaveTypeRepository;

		public UpdateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
			_mapper = mapper;
			_leaveTypeRepository = leaveTypeRepository;
		}

        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
		{
			var validator = new UpdateLeaveTypeCommandValidator(_leaveTypeRepository);
			var validationResult = await validator.ValidateAsync(request);

			if (!validationResult.IsValid)
			{
				throw new BadRequestException("Invalid LeaveType", validationResult);
			}

			var leaveTypeDomain = _mapper.Map<Domain.LeaveType>(request);

			await _leaveTypeRepository.UpdateAsync(leaveTypeDomain);

			return Unit.Value;
		}
	}
}
