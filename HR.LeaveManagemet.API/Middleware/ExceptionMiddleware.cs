using FluentValidation;
using HR.Leave.Management.Application.Exceptions;
using HR.LeaveManagemet.API.Middleware.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HR.LeaveManagemet.API.Middleware
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionMiddleware(RequestDelegate next)
        {
			_next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception ex)
			{
				await HandleExceptionAsync(httpContext, ex);
			}
		}

		private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
		{
			HttpStatusCode defaultCode = HttpStatusCode.InternalServerError;
			CustomProblemDetails problem = new();

			switch (ex)
			{
				case BadRequestException badRequestException:
					defaultCode = HttpStatusCode.BadRequest;
					problem = new CustomProblemDetails()
					{
						Title = badRequestException.Message,
						Status = (int)defaultCode,
						Detail = badRequestException.InnerException?.Message,
						Type = nameof(BadRequestException),
						Errors = badRequestException.ValidationErrors
					};
					break;
				case NotFoundException notFoundException:
					defaultCode = HttpStatusCode.NotFound;
					problem = new CustomProblemDetails()
					{
						Title = notFoundException.Message,
						Status = (int)defaultCode,
						Detail = notFoundException.InnerException?.Message,
						Type = nameof(NotFoundException),
					};
					break;
				default:
					problem = new CustomProblemDetails()
					{
						Title = ex.Message,
						Status = (int)defaultCode,
						Detail = ex.StackTrace,
						Type = nameof(HttpStatusCode.InternalServerError),
					};
					break;
			}

			httpContext.Response.StatusCode = (int)defaultCode;
			await httpContext.Response.WriteAsJsonAsync(problem);
		}
	}
}
