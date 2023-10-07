using AutoMapper;
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

namespace HR.Leave.Management.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest
{
	public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
	{
		private readonly IMapper _mapper;
		private readonly IEmailSender _emailSender;
		private readonly ILeaveRequestRepository _leaveRequestRepository;
		private readonly ILeaveTypeRepository _leaveTypeRepository;
		private readonly IAppLogger<UpdateLeaveRequestCommandHandler> _logger;

		public UpdateLeaveRequestCommandHandler(IMapper mapper,
			IEmailSender emailSender,
			ILeaveRequestRepository leaveRequestRepository,
			ILeaveTypeRepository leaveTypeRepository,
			IAppLogger<UpdateLeaveRequestCommandHandler> logger)
        {
			_mapper = mapper;
			_emailSender = emailSender;
			_leaveRequestRepository = leaveRequestRepository;
			_leaveTypeRepository = leaveTypeRepository;
			_logger = logger;
		}

        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
		{
			var validator = new UpdateLeaveRequestCommandValidator(_leaveTypeRepository, _leaveRequestRepository);
			var validationResult = await validator.ValidateAsync(request);

			if (!validationResult.IsValid)
			{
				throw new BadRequestException("Invalid leave request", validationResult);
			}

			var leaveRequest = await _leaveRequestRepository.GetByIdAsync(request.Id);
			_mapper.Map(request, leaveRequest, typeof(UpdateLeaveRequestCommand), typeof(Domain.LeaveRequest));

			await _leaveRequestRepository.UpdateAsync(leaveRequest);

			try
			{
				var email = new EmailMessage()
				{
					To = string.Empty,//TODO: Get the user email
					Subject = "Leave Request Updated",
					Body = $"Your leave request for {leaveRequest.StartDate:D} to {leaveRequest.EndDate:D} has been updated."
				};

				await _emailSender.SendEmailAsync(email);
			}
			catch(Exception ex)
			{
				_logger.LogWarning("Email sending failed - {0}", ex.Message);
			}
			
			return Unit.Value;
		}
	}
}
