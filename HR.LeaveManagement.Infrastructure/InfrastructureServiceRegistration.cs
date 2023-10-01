using HR.Leave.Management.Application.Contracts.Email;
using HR.Leave.Management.Application.Models.Email;
using HR.LeaveManagement.Infrastructure.EmailService;
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
		public static IServiceCollection ConfigureInfrasturctureServices(this IServiceCollection services,
			IConfiguration configuration)
		{
			services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
			services.AddTransient<IEmailSender, EmailSender>();
			return services;
		}
	}
}
