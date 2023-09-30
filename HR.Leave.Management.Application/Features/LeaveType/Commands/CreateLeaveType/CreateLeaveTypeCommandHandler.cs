using HR.Leave.Management.Application.Contracts.Persistence;
using HR.Leave.Management.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace HR.Leave.Management.Application.Features.LeaveType.Commands.CreateLeaveType
{
	public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, LeaveTypeDto>
	{
		private readonly ILeaveTypeRepository _leaveTypeRepository;
		private readonly IMapper _mapper;

		public CreateLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
			_leaveTypeRepository = leaveTypeRepository;
			_mapper = mapper;
		}

        public async Task<LeaveTypeDto> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
		{
			var leaveTypeDomain = _mapper.Map<Domain.LeaveType>(request);

			await _leaveTypeRepository.CreateAsync(leaveTypeDomain);

			var leaveTypeDto = _mapper.Map<LeaveTypeDto>(leaveTypeDomain);

			return leaveTypeDto;
		}
	}
}
