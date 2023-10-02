using HR.Leave.Management.Application.Exceptions;
using HR.Leave.Management.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.Leave.Management.Application.Features.LeaveType.Commands.DeleteLeaveType;
using HR.Leave.Management.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.Leave.Management.Application.Features.LeaveType.Queries.GetAllLeaveTypeDetais;
using HR.Leave.Management.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagemet.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LeaveTypesController : ControllerBase
	{
		private readonly IMediator _mediator;

		public LeaveTypesController(IMediator mediator)
        {
			_mediator = mediator;
		}

		// GET: api/<LeaveTypesController>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LeaveTypeDto>))]
		public async Task<ActionResult<IEnumerable<LeaveTypeDto>>> GetAll()
		{
			var leaveTypes = await _mediator.Send(new GetLeaveTypesQuery());

			return Ok(leaveTypes);
		}

		// GET api/<LeaveTypesController>/5
		[HttpGet("{id}", Name = nameof(GetById))]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LeaveTypeDetailsDto))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<LeaveTypeDetailsDto>> GetById(int id)
		{
			try
			{
				var leaveTypeDetails = await _mediator.Send(new GetLeaveTypeDetailsQuery(id));
				return Ok(leaveTypeDetails);
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
		}

		// POST api/<LeaveTypesController>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(LeaveTypeDto))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<LeaveTypeDto>> Create([FromBody] CreateLeaveTypeCommand request)
		{
			try
			{
				var leaveType = await _mediator.Send(request);

				return CreatedAtAction(nameof(GetById), new { Id = leaveType.Id }, leaveType);
			}
			catch (BadRequestException)
			{
				return BadRequest();
			}
		}

		// PUT api/<LeaveTypesController>/5
		[HttpPut()]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Put([FromBody] UpdateLeaveTypeCommand request)
		{
			try
			{
				await _mediator.Send(request);
				return NoContent();
			}
			catch(BadRequestException)
			{
				return BadRequest();
			}
		}

		// DELETE api/<LeaveTypesController>/5
		[HttpDelete("{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				await _mediator.Send(new DeleteLeaveTypeCommand(id));
				return NoContent();
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
		}
	}
}
