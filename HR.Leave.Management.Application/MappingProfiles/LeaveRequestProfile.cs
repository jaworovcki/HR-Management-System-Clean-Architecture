using AutoMapper;
using HR.Leave.Management.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HR.Leave.Management.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using HR.Leave.Management.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;
using HR.Leave.Management.Application.Features.LeaveRequest.Queries.GetLeaveRequests;
using HR.Leave.Management.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.MappingProfiles
{
	public class LeaveRequestProfile : Profile
	{
        public LeaveRequestProfile()
        {
            CreateMap<LeaveRequestDto, LeaveRequest>().ReverseMap();
			CreateMap<LeaveRequestDetailsDto, LeaveRequest>().ReverseMap();
			CreateMap<CreateLeaveRequestCommand, LeaveRequest>();
			CreateMap<UpdateLeaveRequestCommand, LeaveRequest>();
        }
    }
}
