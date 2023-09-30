using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Leave.Management.Application.Exceptions
{
	public class BadRequestException : Exception
	{
		public BadRequestException(string mesage, ValidationResult validationResult):
			base(mesage)
		{
			ValidationErrors = new();

			foreach (var validationError in validationResult.Errors)
			{
				ValidationErrors.Add(validationError.ErrorMessage);
			}
		}

        public List<string> ValidationErrors { get; set; }
    }
}
