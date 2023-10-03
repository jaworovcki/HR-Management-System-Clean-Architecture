using FluentValidation;
using HR.Leave.Management.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation
{
	public class DeleteLeaveAllocationCommandValidator : 
		AbstractValidator<DeleteLeaveAllocationCommand>
	{
		private readonly ILeaveAllocationRepository _leaveAllocationRepository;

		public DeleteLeaveAllocationCommandValidator(ILeaveAllocationRepository leaveAllocationRepository)
		{
			RuleFor(p => p.Id)
				.NotNull()
				.NotEmpty().WithMessage("{PropertyName} is required.")
				.GreaterThan(0).WithMessage("{PropertyName} should exeed 0.")
				.MustAsync(IsLeaveAllocationExists);

			_leaveAllocationRepository = leaveAllocationRepository;
		}

		private async Task<bool> IsLeaveAllocationExists(int id, CancellationToken token)
		{
			bool result = await _leaveAllocationRepository.IsAllocationExists(id);

			return result;
		}
	}
	
}
