using AutoMapper;
using HR.Leave.Management.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveRequest.Queries.GetLeaveRequests
{
	public class GetLeaveRequestsQueryHandler :
		IRequestHandler<GetLeaveRequestsQuery, IEnumerable<LeaveRequestDto>>
	{
		private readonly IMapper _mapper;
		private readonly ILeaveRequestRepository _leaveRequestRepository;

		public GetLeaveRequestsQueryHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository)
        {
			_mapper = mapper;
			_leaveRequestRepository = leaveRequestRepository;
		}

        public async Task<IEnumerable<LeaveRequestDto>> Handle(GetLeaveRequestsQuery request, CancellationToken cancellationToken)
		{
			var leaveRequests = await _leaveRequestRepository.GetAllAsync();

			var leaveRequestDtos = _mapper.Map<IEnumerable<LeaveRequestDto>>(leaveRequests);

			return leaveRequestDtos;
		}
	}
}
