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

namespace HR.Leave.Management.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval
{
	public class ChangeLeaveRequestApprovalCommandHandler : 
		IRequestHandler<ChangeLeaveRequestApprovalCommand, Unit>
	{
		private readonly ILeaveRequestRepository _leaveRequestRepository;
		private readonly IAppLogger<ChangeLeaveRequestApprovalCommandHandler> _logger;
		private readonly IEmailSender _emailSender;

		public ChangeLeaveRequestApprovalCommandHandler(ILeaveRequestRepository leaveRequestRepository,
			IAppLogger<ChangeLeaveRequestApprovalCommandHandler> logger,
			IEmailSender emailSender)
		{
			_leaveRequestRepository = leaveRequestRepository;
			_logger = logger;
			_emailSender = emailSender;
		}

		public async Task<Unit> Handle(ChangeLeaveRequestApprovalCommand request, CancellationToken cancellationToken)
		{
			var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);

			if (leaveRequest is null)
			{
				throw new NotFoundException(nameof(LeaveRequest), request.Id);
			}

			leaveRequest.Approved = request.Approved;

			await _leaveRequestRepository.UpdateAsync(leaveRequest);

			_logger.LogInformation($"Leave request {leaveRequest.Id} is {(request.Approved ? "approved" : "rejected")}");

			try
			{
				var email = new EmailMessage()
				{
					To = string.Empty,//TODO: Get the user email
					Subject = $"Leave Request {(request.Approved ? "Approved" : "Rejected")}",
					Body = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been {(request.Approved ? "approved" : "rejected")}."
				};

				await _emailSender.SendEmailAsync(email);
			}
			catch (Exception ex)
			{
				_logger.LogWarning($"Error sending email for leave request {leaveRequest.Id}", ex);
			}

			return Unit.Value;
		}
	}
}
