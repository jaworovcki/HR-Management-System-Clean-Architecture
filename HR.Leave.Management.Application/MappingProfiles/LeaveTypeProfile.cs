using AutoMapper;
using HR.Leave.Management.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.Leave.Management.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.MappingProfiles
{
	public class LeaveTypeProfile : Profile
	{
        public LeaveTypeProfile()
        {
            CreateMap<LeaveTypeDto, LeaveType>().ReverseMap();
        }
    }
}
