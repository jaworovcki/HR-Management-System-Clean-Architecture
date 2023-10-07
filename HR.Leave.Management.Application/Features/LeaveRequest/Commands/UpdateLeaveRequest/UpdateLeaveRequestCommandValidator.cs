using FluentValidation;
using HR.Leave.Management.Application.Contracts.Persistence;
using HR.Leave.Management.Application.Features.LeaveRequest.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest
{
	public class UpdateLeaveRequestCommandValidator :
		AbstractValidator<UpdateLeaveRequestCommand>
	{
		private readonly ILeaveTypeRepository _leaveTypeRepository;
		private readonly ILeaveRequestRepository _leaveRequestRepository;

		public UpdateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepository,
			ILeaveRequestRepository leaveRequestRepository)
        {
			_leaveTypeRepository = leaveTypeRepository;
			_leaveRequestRepository = leaveRequestRepository;

			Include(new BaseLeaveRequestValidator(_leaveTypeRepository));

			RuleFor(p => p.Id)
				.NotNull()
				.NotEmpty().WithMessage("{PropertyName} is required.")
				.GreaterThan(0).WithMessage("{PropertyName} should exeed 0.")
				.MustAsync(IsLeaveRequestExists);
		}

		private async Task<bool> IsLeaveRequestExists(int id, CancellationToken token)
		{
			var leaveRequest = await _leaveRequestRepository.GetByIdAsync(id);

			return leaveRequest != null;
		}
	}
}
