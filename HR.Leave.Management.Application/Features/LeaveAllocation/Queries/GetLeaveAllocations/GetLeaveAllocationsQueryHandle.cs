using AutoMapper;
using HR.Leave.Management.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations
{
	public class GetLeaveAllocationsQueryHandle : IRequestHandler<GetLeaveAllocationsQuery,
		IEnumerable<LeaveAllocationDto>>
	{
		private readonly IMapper _mapper;
		private readonly ILeaveAllocationRepository _leaveAllocationRepository;

		public GetLeaveAllocationsQueryHandle(IMapper mapper, 
			ILeaveAllocationRepository leaveAllocationRepository)
        {
			_mapper = mapper;
			_leaveAllocationRepository = leaveAllocationRepository;
		}
        public async Task<IEnumerable<LeaveAllocationDto>> Handle(GetLeaveAllocationsQuery request, CancellationToken cancellationToken)
		{
			var leaveAllocations = await _leaveAllocationRepository.GetLeaveAllocationsWithDetails();

			var leaveAllocationDtos = _mapper.Map<IEnumerable<LeaveAllocationDto>>(leaveAllocations);

			return leaveAllocationDtos;
		}
	}
}
