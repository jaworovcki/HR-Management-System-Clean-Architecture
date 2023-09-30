using AutoMapper;
using HR.Leave.Management.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveType.Queries.GetAllLeaveTypeDetais
{
	public class GetLeaveTypeDetailsQueryHandle : IRequestHandler<GetLeaveTypeDetailsQuery,
		LeaveTypeDetailsDto>
	{
		private readonly IMapper _mapper;
		private readonly ILeaveTypeRepository _leaveTypeRepository;

		public GetLeaveTypeDetailsQueryHandle(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
        {
			_mapper = mapper;
			_leaveTypeRepository = leaveTypeRepository;
		}

        public async Task<LeaveTypeDetailsDto> Handle(GetLeaveTypeDetailsQuery request, CancellationToken cancellationToken)
		{
			var leaveTypeDetails = await _leaveTypeRepository.GetByIdAsync(request.Id);

			var leaveTypeDetailsDto = _mapper.Map<LeaveTypeDetailsDto>(leaveTypeDetails);

			return leaveTypeDetailsDto;
		}
	}
}
