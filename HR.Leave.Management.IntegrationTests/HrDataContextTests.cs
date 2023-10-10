using HR.Leave.Management.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HR.Leave.Management.IntegrationTests;

public class HrDataContextTests
{
    private readonly HrDataContext _dbContext;

    public HrDataContextTests()
    {
        var dbOptions = new DbContextOptionsBuilder<HrDataContext>()
            .UseInMemoryDatabase(databaseName: "HrDb")
            .Options;

        _dbContext = new HrDataContext(dbOptions);
    }

    [Fact]
    public async Task SaveSetDateCreatedValue()
    {
        // Arrange
        var leaveType = new LeaveType
        {
            Id = 1,
            DefaultDays = 10,
            Name = "Test Leave Type",
        };

        // Act

        _dbContext.LeaveTypes.Add(leaveType);
        await _dbContext.SaveChangesAsync();

        // Assert

        leaveType.DateCreated.ShouldNotBeNull();
    }
    
    [Fact]
    public async Task SaveSetDateModifiedValue()
    {
        // Arrange
        var leaveType = new LeaveType
        {
            Id = 2,
            DefaultDays = 10,
            Name = "Test Leave Type",
        };

        // Act

        _dbContext.LeaveTypes.Add(leaveType);
        await _dbContext.SaveChangesAsync();

        // Assert

        leaveType.DateModified.ShouldNotBeNull();
    }
}
