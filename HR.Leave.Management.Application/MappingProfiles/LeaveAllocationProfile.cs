using AutoMapper;
using HR.Leave.Management.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.Leave.Management.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR.Leave.Management.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using HR.Leave.Management.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
using HR.Leave.Management.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.MappingProfiles
{
    public class LeaveAllocationProfile : Profile
	{
        public LeaveAllocationProfile()
        {
            CreateMap<LeaveAllocationDto, LeaveAllocation>().ReverseMap();
            CreateMap<LeaveAllocationDetailsDto, LeaveAllocation>().ReverseMap();
            CreateMap<CreateLeaveAllocationCommand, LeaveAllocation>();
            CreateMap<UpdateLeaveAllocationCommand, LeaveAllocation>();
        }
    }
}
