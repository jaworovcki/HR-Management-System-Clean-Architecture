using AutoMapper;
using HR.Leave.Management.Application.Contracts.Email;
using HR.Leave.Management.Application.Contracts.Logging;
using HR.Leave.Management.Application.Contracts.Persistence;
using HR.Leave.Management.Application.Exceptions;
using HR.Leave.Management.Application.Features.LeaveRequest.Queries.GetLeaveRequests;
using HR.Leave.Management.Application.Models.Email;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveRequest.Commands.CreateLeaveRequest
{
	public class CreateLeaveRequestCommandHandler :
		IRequestHandler<CreateLeaveRequestCommand, LeaveRequestDto>
	{
		private readonly ILeaveRequestRepository _leaveRequestRepository;
		private readonly ILeaveTypeRepository _leaveTypeRepository;
		private readonly IMapper _mapper;
		private readonly IAppLogger<CreateLeaveRequestCommandHandler> _appLogger;
		private readonly IEmailSender _emailSender;

		public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,
			ILeaveTypeRepository leaveTypeRepository,
			IMapper mapper,
			IAppLogger<CreateLeaveRequestCommandHandler> appLogger,
			IEmailSender emailSender)
        {
			_leaveRequestRepository = leaveRequestRepository;
			_leaveTypeRepository = leaveTypeRepository;
			_mapper = mapper;
			_appLogger = appLogger;
			_emailSender = emailSender;
		}

        public async Task<LeaveRequestDto> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
		{
			var validator = new CreateLeaveRequestCommandValidator(_leaveTypeRepository);
			var validationResult = await validator.ValidateAsync(request);

			if (!validationResult.IsValid)
			{
				_appLogger.LogWarning("Validation error in create request for {0} - {1}",
															nameof(LeaveRequest), request.LeaveTypeId);
				throw new BadRequestException("Invalid leave request", validationResult);
			}

			var leaveRequestEntity = _mapper.Map<Domain.LeaveRequest>(request);

			Console.WriteLine($"Leave request entity comment: {leaveRequestEntity.RequestComment}");

			await _leaveRequestRepository.CreateAsync(leaveRequestEntity);

			_appLogger.LogInformation($"{nameof(LeaveRequest)} with Id " +
									$"{leaveRequestEntity.Id} is successfully created.");

			try
			{
				var email = new EmailMessage()
				{
					To = string.Empty,//TODO: Get the user email
					Subject = "Leave Request Submitted",
					Body = $"Your leave request for {leaveRequestEntity.StartDate:D} to {leaveRequestEntity.EndDate:D} has been submitted successfully."
				};

				await _emailSender.SendEmailAsync(email);
			}
			catch (Exception ex)
			{
				_appLogger.LogWarning("Mailing about leave request failed.", ex);
			}

			var leaveRequestDto = _mapper.Map<LeaveRequestDto>(leaveRequestEntity);

			return leaveRequestDto;
		}
	}
}
