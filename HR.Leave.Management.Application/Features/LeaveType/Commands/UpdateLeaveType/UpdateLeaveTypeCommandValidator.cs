﻿using FluentValidation;
using HR.Leave.Management.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Features.LeaveType.Commands.UpdateLeaveType
{
	public class UpdateLeaveTypeCommandValidator : 
		AbstractValidator<UpdateLeaveTypeCommand>
	{
		private readonly ILeaveTypeRepository _leaveTypeRepository;

		public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
        {
			RuleFor(p => p.Id)
				.NotNull()
				.GreaterThan(0).WithMessage("{PropertyName} is required.")
				.MustAsync(LeaveTypeExists);

            RuleFor(p => p.Name)
				.NotEmpty().WithMessage("{PropertyName} is required.")
				.NotNull()
				.MaximumLength(70).WithMessage("{PropertyName} must not exceed 70 characters.");

			RuleFor(p => p.DefaultDays)
				.GreaterThan(1).WithMessage("{PropertyName} must be greater than 1.")
				.LessThan(100).WithMessage("{PropertyName} must be less than 100.");

			RuleFor(p => p)
				.MustAsync(LeaveTypeUnique)
				.WithMessage("Leave type with the same name already exists.");

			_leaveTypeRepository = leaveTypeRepository;
		}

		private Task<bool> LeaveTypeExists(int id, CancellationToken token)
		{
			return _leaveTypeRepository.IsLeaveTypeExists(id);
		}

		private Task<bool> LeaveTypeUnique(UpdateLeaveTypeCommand command, CancellationToken token)
		{
			return _leaveTypeRepository.IsLeaveTypeUnique(command.Name, command.Id);
		}
	}
}
