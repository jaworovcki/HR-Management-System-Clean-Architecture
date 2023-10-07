using HR.Leave.Management.Application.Exceptions;
using HR.Leave.Management.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.Leave.Management.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;
using HR.Leave.Management.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR.Leave.Management.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;
using HR.Leave.Management.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations;
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
	public class LeaveAllocationsController : ControllerBase
	{
		private readonly IMediator _mediator;

		public LeaveAllocationsController(IMediator mediator)
        {
			_mediator = mediator;
		}

        // GET: api/<LeaveAllocationController>
        [HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<LeaveAllocationDto>))]
		public async Task<ActionResult<IEnumerable<LeaveAllocationDto>>> GetAll()
		{
			var leaveAllocations = await _mediator.Send(new GetLeaveAllocationsQuery());

			return Ok(leaveAllocations);
		}

		// GET api/<LeaveAllocationController>/5
		[HttpGet, Route("{id}")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LeaveAllocationDetailsDto))]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<LeaveAllocationDetailsDto>> GetById(int id)
		{
			try
			{
				var leaveAllocationDetailsDto = await _mediator.Send(new GetLeaveAllocationDetaisQuery { Id = id });
				return Ok(leaveAllocationDetailsDto);
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
		}

		// POST api/<LeaveAllocationController>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(LeaveAllocationDto))]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<LeaveAllocationDto>> Post([FromBody] CreateLeaveAllocationCommand request)
		{
			try
			{
				var leaveAllocation = await _mediator.Send(request);

				return CreatedAtAction(nameof(GetById), new { Id = leaveAllocation.Id }, leaveAllocation);
			}
			catch (BadRequestException)
			{
				return BadRequest();
			}
		}

		// PUT api/<LeaveTypesController>/5
		[HttpPut]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<IActionResult> Put([FromBody] UpdateLeaveAllocationCommand request)
		{
			try
			{
				await _mediator.Send(request);
				return NoContent();
			}
			catch (BadRequestException)
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
				await _mediator.Send(new DeleteLeaveAllocationCommand { Id = id });
				return NoContent();
			}
			catch (NotFoundException)
			{
				return NotFound();
			}
		}
	}
}
