using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketingSystem.Application.Common.Mediator;

using TicketingSystem.Application.Ticketing.Commands;
using TicketingSystem.Application.Ticketing.Queries;



namespace TicketingSystem.Api.Controllers
{
    [ApiController]
    [Route("tickets")]
    public class TicketController : ControllerBase
    {
        private readonly SimpleMediator _mediator;

        public TicketController(SimpleMediator mediator)
        {
            _mediator = mediator;
        }

        // POST /tickets – Create a new ticket (Employee only)
        [HttpPost]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Create([FromBody] CreateTicketCommand command)
        {
            command.CreatedByUserId = GetUserId();
            var ticketId = await _mediator.Send(command);
            return Ok(new { ticketId });
        }

        // GET /tickets/my – List tickets created by the current user (Employee)
        [HttpGet("my")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GetMyTickets()
        {
            var query = new GetMyTicketsQuery { UserId = GetUserId() };
            var tickets = await _mediator.Send(query);
            return Ok(tickets);
        }

        // GET /tickets – List all tickets (Admin only)
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllTickets()
        {
            var tickets = await _mediator.Send(new GetAllTicketsQuery());
            return Ok(tickets);
        }

        // PUT /tickets/{id} – Update ticket status and assignment (Admin only)
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTicketCommand command)
        {
            command.TicketId = id;
            var result = await _mediator.Send(command);
            return result ? Ok("Ticket updated") : NotFound("Ticket not found");
        }

        // GET /tickets/stats – Show ticket counts by status (Admin only)
        [HttpGet("stats")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetStats()
        {
            var stats = await _mediator.Send(new GetTicketStatsQuery());
            return Ok(stats);
        }

        private Guid GetUserId()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");
            return Guid.TryParse(userId, out var id)
                ? id
                : throw new UnauthorizedAccessException("User ID not found");
        }
    }
}

