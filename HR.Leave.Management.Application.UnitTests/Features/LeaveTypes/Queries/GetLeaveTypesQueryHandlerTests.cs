using AutoMapper;
using HR.Leave.Management.Application.Contracts.Logging;
using HR.Leave.Management.Application.Contracts.Persistence;
using HR.Leave.Management.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.Leave.Management.Application.MappingProfiles;
using HR.Leave.Management.Application.UnitTests.Mock;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.UnitTests.Features.LeaveTypes.Queries
{
	public class GetLeaveTypesQueryHandlerTests
	{
        private readonly Mock<ILeaveTypeRepository> _mockRepo;
		private IMapper _mapper;
		private Mock<IAppLogger<GetLeaveTypesQueryHandler>> _mockAppLogger;

		public GetLeaveTypesQueryHandlerTests()
        {
            _mockRepo = MockLeaveTypeRepository.GetMockLeaveTypeRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<LeaveTypeProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _mockAppLogger = new Mock<IAppLogger<GetLeaveTypesQueryHandler>>();
        }

        [Fact]
        public async Task GetLeaveTypeListTest()
        {
            //Arrange

            var handler = new GetLeaveTypesQueryHandler(_mapper, _mockRepo.Object, _mockAppLogger.Object);

            //Act

            var result = await handler.Handle(new GetLeaveTypesQuery(), CancellationToken.None);

            //Assert

            result.ShouldBeOfType<List<LeaveTypeDto>>();
            result.Count().ShouldBe(3);
        }
    }
}
