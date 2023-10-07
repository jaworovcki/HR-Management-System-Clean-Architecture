using AutoMapper;
using HR.Leave.Management.Application.Contracts.Persistence;
using HR.Leave.Management.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails
{
	public class GetLeaveRequestDetailsQueryHandler :
		IRequestHandler<GetLeaveRequestDetailsQuery, LeaveRequestDetailsDto>
	{
		private readonly IMapper _mapper;
		private readonly ILeaveRequestRepository _leaveRequestRepository;

		public GetLeaveRequestDetailsQueryHandler(IMapper mapper,
			ILeaveRequestRepository leaveRequestRepository)
        {
			_mapper = mapper;
			_leaveRequestRepository = leaveRequestRepository;
		}
        public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailsQuery request, CancellationToken cancellationToken)
		{
			var leaveRequest = await _leaveRequestRepository.GetLeaveRequestWithDetailsAsync(request.Id);

			if (leaveRequest is null)
			{
				throw new NotFoundException("Leave request not found", nameof(request.Id));
			}

			var leaveRequestDetailsDto = _mapper.Map<LeaveRequestDetailsDto>(leaveRequest);

			return leaveRequestDetailsDto;
		}
	}
}
