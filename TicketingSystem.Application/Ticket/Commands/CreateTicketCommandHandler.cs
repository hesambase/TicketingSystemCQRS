using TicketingSystem.Application.Common.Mediator;
using TicketingSystem.Application.Ticketing.Commands;
using TicketingSystem.Domain.Enums;
using TicketingSystem.Shared.Interfaces;

namespace TicketingSystem.Application.Ticketing.Commands
{
    public class CreateTicketCommandHandler : ICommandHandler<CreateTicketCommand, Guid>
    {
        private readonly IAppDbContext _db;

        public CreateTicketCommandHandler(IAppDbContext db)
        {
            _db = db;
        }

        public async Task<Guid> Handle(CreateTicketCommand command, CancellationToken cancellationToken)
        {
            var ticket = new Ticket
            {
                Id = Guid.NewGuid(),
                Title = command.Title,
                Description = command.Description,
                CreatedByUserId = command.CreatedByUserId,
                Priority = command.Priority,
                Status = TicketStatus.Open,
                CreatedAt = DateTime.UtcNow
            };

            _db.Tickets.Add(ticket);
            await _db.SaveChangesAsync(cancellationToken);

            return ticket.Id;
        }
    }

}
