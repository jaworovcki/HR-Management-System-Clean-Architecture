using AutoMapper;
using HR.Leave.Management.Application.Contracts.Logging;
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
		private readonly IAppLogger<UpdateLeaveTypeCommandHandler> _logger;

		public UpdateLeaveTypeCommandHandler(IMapper mapper,
			ILeaveTypeRepository leaveTypeRepository,
			IAppLogger<UpdateLeaveTypeCommandHandler> logger)
        {
			_mapper = mapper;
			_leaveTypeRepository = leaveTypeRepository;
			_logger = logger;
		}

        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
		{
			var validator = new UpdateLeaveTypeCommandValidator(_leaveTypeRepository);
			var validationResult = await validator.ValidateAsync(request);

			if (!validationResult.IsValid)
			{
				_logger.LogWarning($"Validation error in update request for {0} - {1}",
					nameof(LeaveType), request.Id);

				throw new BadRequestException("Invalid LeaveType", validationResult);
			}

			var leaveTypeDomain = _mapper.Map<Domain.LeaveType>(request);

			await _leaveTypeRepository.UpdateAsync(leaveTypeDomain);

			return Unit.Value;
		}
	}
}
