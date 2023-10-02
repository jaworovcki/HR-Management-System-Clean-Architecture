using HR.Leave.Management.Application.Contracts.Email;
using HR.Leave.Management.Application.Contracts.Logging;
using HR.Leave.Management.Application.Models.Email;
using HR.LeaveManagement.Infrastructure.EmailService;
using HR.LeaveManagement.Infrastructure.LoggingService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Infrastructure
{
	public static class InfrastructureServiceRegistration
	{
		public static IServiceCollection AddInfrasturctureServices(this IServiceCollection services,
			IConfiguration configuration)
		{
			services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));

			services.AddTransient<IEmailSender, EmailSender>();

			services.AddScoped(typeof(IAppLogger<>), typeof(LoggingAdapter<>));

			return services;
		}
	}
}
