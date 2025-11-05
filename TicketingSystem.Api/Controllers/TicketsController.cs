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
        private readonly ICommandBus _bus;

        public TicketController(ICommandBus bus)
        {
            _bus = bus;
        }

        // POST /tickets – Create a new ticket (Employee only)
        [HttpPost("CreateTicket")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> Create([FromBody] CreateTicketCommand command)
        {
            command.CreatedByUserId = GetUserId();
            var ticketId = await _bus.Send(command);
            return Ok(new { ticketId });
        }

        // GET /tickets/my – List tickets created by the current user (Employee)
        [HttpGet("myTickets")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> GetMyTickets()
        {
            var query = new GetMyTicketsQuery { UserId = GetUserId() };
            var tickets = await _bus.Send(query);
            return Ok(tickets);
        }

        // GET /tickets – List all tickets (Admin only)
        [HttpGet("GetAllTickets")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllTickets()
        {
            var tickets = await _bus.Send(new GetAllTicketsQuery());
            return Ok(tickets);
        }

        // PUT /tickets/{id} – Update ticket status and assignment (Admin only)
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTicketCommand command)
        {
            command.TicketId = id;
            var result = await _bus.Send(command);
            return result ? Ok("Ticket updated") : NotFound("Ticket not found");
        }

        // GET /tickets/stats – Show ticket counts by status (Admin only)
        [HttpGet("stats")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetStats()
        {
            var stats = await _bus.Send(new GetTicketStatsQuery());
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

