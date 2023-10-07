using HR.Leave.Management.Application.Contracts.Email;
using HR.Leave.Management.Application.Models.Email;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Infrastructure.EmailService
{
	public class EmailSender : IEmailSender
	{
        public EmailSender(IOptions<EmailSettings> options)
        {
			Options = options.Value;
		}

		public EmailSettings Options { get; }

		public async Task<bool> SendEmailAsync(EmailMessage email)
		{
			var client = new SendGridClient(Options.ApiKey);
			var to = new EmailAddress(email.To);
			var from = new EmailAddress()
			{
				Email = Options.FromAddress,
				Name = Options.FromName
			};
			var message = MailHelper.CreateSingleEmail(from, to, email.Subject, email.Body, 
				email.Body);

			var response = await client.SendEmailAsync(message);

			return response.IsSuccessStatusCode;
		}
	}
}
