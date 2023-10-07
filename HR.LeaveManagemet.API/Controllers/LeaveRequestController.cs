using HR.Leave.Management.Application.Exceptions;
using HR.Leave.Management.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;
using HR.Leave.Management.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;
using HR.Leave.Management.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HR.Leave.Management.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;
using HR.Leave.Management.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using HR.Leave.Management.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;
using HR.Leave.Management.Application.Features.LeaveRequest.Queries.GetLeaveRequests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagemet.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LeaveRequestController : ControllerBase
	{
		private readonly IMediator _mediator;

		public LeaveRequestController(IMediator mediator)
        {
			_mediator = mediator;
		}

        // GET: api/<LeaveRequestController>
        [HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LeaveRequestDto>))]
		public async Task<ActionResult<IEnumerable<LeaveRequestDto>>> GetAll()
		{
			var leaveRequestDtos = await _mediator.Send(new GetLeaveRequestsQuery());

			return Ok(leaveRequestDtos);
		}

		// GET api/<LeaveRequestController>/5
		[HttpGet, Route("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LeaveRequestDetailsDto))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<LeaveRequestDetailsDto>> GetById([FromRoute] int id)
		{
			var leaveRequestDetailsDto = await _mediator.Send(new GetLeaveRequestDetailsQuery { Id = id });

			return Ok(leaveRequestDetailsDto);
		}

		// POST api/<LeaveRequestController>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(LeaveRequestDto))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<LeaveRequestDto>> Post([FromBody] CreateLeaveRequestCommand request)
		{
			var leaveRequestDto = await _mediator.Send(request);

			return CreatedAtAction(nameof(GetById), new { Id = leaveRequestDto.Id }, leaveRequestDto);
		}

		// PUT api/<LeaveRequestController>/5
		[HttpPut]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Put([FromBody] UpdateLeaveRequestCommand request)
		{
			await _mediator.Send(request);

			return NoContent();
		}

		// DELETE api/<LeaveRequestController>/5
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Delete(int id)
		{
			await _mediator.Send(new DeleteLeaveRequestCommand { Id = id });

			return NoContent();
		}

		[HttpPut("cancel-request")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> CancelRequest([FromRoute] CancelLeaveRequestCommand request)
		{
			await _mediator.Send(request);

			return NoContent();
		}

		[HttpPut("approve-request")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> UpdateRequestApproval([FromBody] ChangeLeaveRequestApprovalCommand request)
		{

			await _mediator.Send(request);

			return NoContent();
		}
	}
}
