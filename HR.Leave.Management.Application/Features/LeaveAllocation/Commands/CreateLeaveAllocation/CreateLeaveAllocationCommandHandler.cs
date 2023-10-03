using AutoMapper;
using HR.Leave.Management.Application.Contracts.Logging;
using HR.Leave.Management.Application.Contracts.Persistence;
using HR.Leave.Management.Application.Exceptions;
using HR.Leave.Management.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandHandler :
        IRequestHandler<CreateLeaveAllocationCommand, LeaveAllocationDto>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IAppLogger<CreateLeaveAllocationCommandHandler> _logger;

        public CreateLeaveAllocationCommandHandler(IMapper mapper,
            ILeaveAllocationRepository leaveAllocationRepository,
            ILeaveTypeRepository leaveTypeRepository,
            IAppLogger<CreateLeaveAllocationCommandHandler> logger)
        {
            _mapper = mapper;
            _leaveAllocationRepository = leaveAllocationRepository;
            _leaveTypeRepository = leaveTypeRepository;
            _logger = logger;
        }

        public async Task<LeaveAllocationDto> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation error in create request for {0} - {1}",
                                        nameof(LeaveAllocation), request.LeaveTypeId);
                throw new BadRequestException("Invalid LeaveType Id", validationResult);
            }

            var leaveAllocation = _mapper.Map<Domain.LeaveAllocation>(request);
            await _leaveAllocationRepository.CreateAsync(leaveAllocation);

            _logger.LogInformation($"{nameof(LeaveAllocation)} with Id " +
                $"{leaveAllocation.Id} is successfully created.");

            var leaveAllocationDto = _mapper.Map<LeaveAllocationDto>(leaveAllocation);

            return leaveAllocationDto;
        }
    }
}
