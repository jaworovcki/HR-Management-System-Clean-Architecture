using HR.Leave.Management.Application.Contracts.Persistence;
using HR.LeaveManagement.Persistence.DatabaseContext;
using HR.LeaveManagement.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Persistence
{
	public static class PersistenceServiceRegistration
	{
		public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<HrDataContext>(options =>
				options.UseSqlServer(configuration.GetConnectionString("HrLeaveManagementConnectionString")));

			services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

			services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();

			services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();

			services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();

			return services;
		}
	}
}
