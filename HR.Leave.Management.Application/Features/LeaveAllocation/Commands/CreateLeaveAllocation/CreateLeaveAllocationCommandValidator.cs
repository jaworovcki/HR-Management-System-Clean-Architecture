using FluentValidation;
using HR.Leave.Management.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandValidator :
        AbstractValidator<CreateLeaveAllocationCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveAllocationCommandValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            RuleFor(p => p.LeaveTypeId)
                .NotNull()
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .GreaterThan(0).WithMessage("{PropertyName} should exeed 0.")
                .MustAsync(IsLeaveTypeExists);

            _leaveTypeRepository = leaveTypeRepository;
        }

        private async Task<bool> IsLeaveTypeExists(int id, CancellationToken token)
        {
            bool result = await _leaveTypeRepository.IsLeaveTypeExists(id);

            return result;
        }
    }
}
