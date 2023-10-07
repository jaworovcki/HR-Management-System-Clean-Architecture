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
			ValidationErrors = validationResult.Errors
				.GroupBy(x => x.PropertyName, x => x.ErrorMessage)
				.ToDictionary(x => x.Key, x => x.ToArray());
		}

        public IDictionary<string, string[]> ValidationErrors { get; set; }
    }
}
