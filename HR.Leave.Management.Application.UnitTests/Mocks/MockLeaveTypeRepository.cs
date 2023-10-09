using HR.Leave.Management.Application.Contracts.Persistence;
using HR.Leave.Management.Domain;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.UnitTests.Mock
{
	public class MockLeaveTypeRepository
	{
		public static Mock<ILeaveTypeRepository> GetMockLeaveTypeRepository()
		{
			var leaveTypes = new List<LeaveType>()
			{
				new LeaveType
				{
					Id = 1,
					Name = "Annual",
					DateCreated = DateTime.Now,
					DefaultDays = 10
				},

				new LeaveType
				{
					Id = 2,
					Name = "Sick",
					DateCreated = DateTime.Now,
					DefaultDays = 10
				},

				new LeaveType
				{
					Id = 3,
					Name = "Maternity",
					DateCreated = DateTime.Now,
					DefaultDays = 10
				},
			};

			var mockLeaveTypeRepository = new Mock<ILeaveTypeRepository>();

			mockLeaveTypeRepository.Setup(repo => repo.GetAllAsync())
				.ReturnsAsync(leaveTypes);

			mockLeaveTypeRepository.Setup(repo => repo.CreateAsync(It.IsAny<LeaveType>())).ReturnsAsync(
				(LeaveType leaveType) =>
				{
					leaveTypes.Add(leaveType);

					return leaveType;
				});

			return mockLeaveTypeRepository;
		}
	}
}
