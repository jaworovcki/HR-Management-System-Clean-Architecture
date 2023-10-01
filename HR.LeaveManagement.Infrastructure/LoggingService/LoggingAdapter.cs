using HR.Leave.Management.Application.Contracts.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Infrastructure.LoggingService
{
	public class LoggingAdapter<T> : IAppLogger<T>
	{
		private readonly ILogger<T> _logger;

		public LoggingAdapter(ILoggerFactory loggerFactory)
        {
			_logger = loggerFactory.CreateLogger<T>();
		}

        public void LogInformation(string message, params object[] args)
		{
			_logger.LogInformation(message, args);
		}

		public void LogWarning(string message, params object[] args)
		{
			_logger.LogWarning(message, args);
		}
	}
}
