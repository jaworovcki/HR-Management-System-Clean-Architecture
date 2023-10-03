using FluentValidation;
using HR.Leave.Management.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
	public class UpdateLeaveAllocationCommandValidator : 
		AbstractValidator<UpdateLeaveAllocationCommand>
	{
		private readonly ILeaveAllocationRepository _leaveAllocationRepository;
		private readonly ILeaveTypeRepository _leaveTypeRepository;

		public UpdateLeaveAllocationCommandValidator(ILeaveAllocationRepository leaveAllocationRepository,
			ILeaveTypeRepository leaveTypeRepository)
        {
            RuleFor(p => p.NumberOfDays)
				.NotNull()
				.NotEmpty().WithMessage("{PropertyName} is required.")
				.GreaterThan(0).WithMessage("{PropertyName} should exeed 0.");

			RuleFor(p => p.Period)
				.NotNull()
				.NotEmpty().WithMessage("{PropertyName} is required.")
				.GreaterThan(0).WithMessage("{PropertyName} should exeed 0.");

			RuleFor(p => p.Id)
				.NotNull()
				.NotEmpty().WithMessage("{PropertyName} is required.")
				.GreaterThan(0).WithMessage("{PropertyName} should exeed 0.")
				.MustAsync(IsLeaveAllocationExists);

			RuleFor(p => p.LeaveTypeId)
				.NotNull()
				.NotEmpty().WithMessage("{PropertyName} is required.")
				.GreaterThan(0).WithMessage("{PropertyName} should exeed 0.")
				.MustAsync(IsLeaveTypeExists);

			_leaveAllocationRepository = leaveAllocationRepository;
			_leaveTypeRepository = leaveTypeRepository;
		}

		private async Task<bool> IsLeaveTypeExists(int id, CancellationToken token)
			=> await _leaveTypeRepository.IsLeaveTypeExists(id);

		private async Task<bool> IsLeaveAllocationExists(int id, CancellationToken token)
			=> await _leaveAllocationRepository.IsAllocationExists(id);
	}
}
