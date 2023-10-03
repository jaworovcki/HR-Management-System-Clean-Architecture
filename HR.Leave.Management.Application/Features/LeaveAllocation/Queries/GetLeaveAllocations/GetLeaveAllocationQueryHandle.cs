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
	public class GetLeaveAllocationQueryHandle : IRequestHandler<GetLeaveAllocationQuery,
		IEnumerable<LeaveAllocationDto>>
	{
		private readonly IMapper _mapper;
		private readonly ILeaveAllocationRepository _leaveAllocationRepository;

		public GetLeaveAllocationQueryHandle(IMapper mapper, 
			ILeaveAllocationRepository leaveAllocationRepository)
        {
			_mapper = mapper;
			_leaveAllocationRepository = leaveAllocationRepository;
		}
        public async Task<IEnumerable<LeaveAllocationDto>> Handle(GetLeaveAllocationQuery request, CancellationToken cancellationToken)
		{
			var leaveAllocations = await _leaveAllocationRepository.GetLeaveAllocationsWithDetails();

			var leaveAllocationDtos = _mapper.Map<IEnumerable<LeaveAllocationDto>>(leaveAllocations);

			return leaveAllocationDtos;
		}
	}
}
