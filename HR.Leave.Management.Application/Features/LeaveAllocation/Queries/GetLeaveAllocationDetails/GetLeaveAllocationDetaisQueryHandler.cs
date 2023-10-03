using AutoMapper;
using HR.Leave.Management.Application.Contracts.Persistence;
using HR.Leave.Management.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails
{
	public class GetLeaveAllocationDetaisQueryHandler : IRequestHandler<GetLeaveAllocationDetaisQuery,
		LeaveAllocationDetailsDto>
	{
		private readonly IMapper _mapper;
		private readonly ILeaveAllocationRepository _leaveAllocationRepository;

		public GetLeaveAllocationDetaisQueryHandler(IMapper mapper,
			ILeaveAllocationRepository leaveAllocationRepository)
		{
			_mapper = mapper;
			_leaveAllocationRepository = leaveAllocationRepository;
		}

		public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationDetaisQuery request,
			CancellationToken cancellationToken)
		{
			var leaveAllocationDetails = await _leaveAllocationRepository
				.GetLeaveAllocationWithDetails(request.Id);

			if (leaveAllocationDetails is null) 
			{
				throw new NotFoundException(nameof(LeaveAllocation), request.Id);
			}

			var leaveAllocationDetailsDto = _mapper.Map<LeaveAllocationDetailsDto>(leaveAllocationDetails);

			return leaveAllocationDetailsDto;
		}
	}
}
