using HR.Leave.Management.Application.Contracts.Email;
using HR.Leave.Management.Application.Contracts.Logging;
using HR.Leave.Management.Application.Contracts.Persistence;
using HR.Leave.Management.Application.Exceptions;
using HR.Leave.Management.Application.Models.Email;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveRequest.Commands.CancelLeaveRequest
{
	public class CancelLeaveRequestCommandHandler : IRequestHandler<CancelLeaveRequestCommand, Unit>
	{
		private readonly ILeaveRequestRepository _leaveRequestRepository;
		private readonly IEmailSender _emailSender;
		private readonly IAppLogger<CancelLeaveRequestCommandHandler> _logger;

		public CancelLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,
			IEmailSender emailSender,
			IAppLogger<CancelLeaveRequestCommandHandler> logger)
		{
			_leaveRequestRepository = leaveRequestRepository;
			_emailSender = emailSender;
			_logger = logger;
		}

		public async Task<Unit> Handle(CancelLeaveRequestCommand request, CancellationToken cancellationToken)
		{
			var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

			if (leaveRequest is null)
			{
				throw new NotFoundException(nameof(LeaveRequest), request.Id);
			}

			leaveRequest.Cancelled = true;

			await _leaveRequestRepository.UpdateAsync(leaveRequest);

			try
			{
				var email = new EmailMessage()
				{
					To = string.Empty,//TODO: Get the user email
					Subject = "Leave Request Cancelled",
					Body = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been cancelled."
				};
			}
			catch(Exception ex)
			{
				_logger.LogWarning($"Error sending email for leave request {leaveRequest.Id}", ex);
			}
			return Unit.Value;

		}
	}
}
