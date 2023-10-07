using FluentValidation;
using HR.Leave.Management.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveRequest.Shared
{
	public class BaseLeaveRequestValidator :
		AbstractValidator<BaseLeaveRequest>
	{
		private readonly ILeaveTypeRepository _leaveTypeRepository;

		public BaseLeaveRequestValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            RuleFor(p => p.StartDate)
                .LessThan(p => p.EndDate)
                .WithMessage("{PropertyName} must be before {ComparisonValue}");

            RuleFor(p => p.EndDate)
                .GreaterThan(p => p.StartDate)
				.WithMessage("{PropertyName} must be after {ComparisonValue}");

            RuleFor(p => p.LeaveTypeId)
                .GreaterThan(0)
                .WithMessage("{PropertyName} should exeed 0.")
                .MustAsync(IsLeaveTypeExists)
                .WithMessage("{PropertyName} does not exist.");

			_leaveTypeRepository = leaveTypeRepository;
		}

		private async Task<bool> IsLeaveTypeExists(int id, CancellationToken token) 
            => await _leaveTypeRepository.IsLeaveTypeExists(id);
	}
}
