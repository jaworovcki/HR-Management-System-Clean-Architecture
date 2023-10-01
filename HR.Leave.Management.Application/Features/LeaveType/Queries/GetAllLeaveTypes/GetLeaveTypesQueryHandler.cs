using AutoMapper;
using HR.Leave.Management.Application.Contracts.Logging;
using HR.Leave.Management.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
	public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQuery,
		IEnumerable<LeaveTypeDto>>
	{
		private readonly IMapper _mapper;
		private readonly ILeaveTypeRepository _leaveTypeRepository;
		private readonly IAppLogger<GetLeaveTypesQueryHandler> _logger;

		public GetLeaveTypesQueryHandler(IMapper mapper,
			ILeaveTypeRepository leaveTypeRepository,
			IAppLogger<GetLeaveTypesQueryHandler> logger)
		{
			_mapper = mapper;
			_leaveTypeRepository = leaveTypeRepository;
			_logger = logger;
		}
		
		public async Task<IEnumerable<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
		{
			var leaveTypes = await _leaveTypeRepository.GetAllAsync();

			var leaveTypesDto = _mapper.Map<IEnumerable<LeaveTypeDto>>(leaveTypes);

			_logger.LogInformation($"Request for all Leave Types was handled successfully");

			return leaveTypesDto;
		}
	}
}
