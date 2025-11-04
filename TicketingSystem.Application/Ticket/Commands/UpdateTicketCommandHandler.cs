using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Application.Common.Mediator;
using TicketingSystem.Shared.Interfaces;

namespace TicketingSystem.Application.Ticketing.Commands
{
    public class UpdateTicketCommandHandler : ICommandHandler<UpdateTicketCommand, bool>
    {
        private readonly IAppDbContext _db;

        public UpdateTicketCommandHandler(IAppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Handle(UpdateTicketCommand command, CancellationToken cancellationToken)
        {
            var ticket = await _db.Tickets.FindAsync(new object[] { command.TicketId }, cancellationToken);
            if (ticket is null) return false;
            if (command.Priority.HasValue)
                ticket.Priority = command.Priority.Value;
            ticket.Status = command.Status;
            ticket.AssignedToUserId = command.AssignedToUserId;

            await _db.SaveChangesAsync(cancellationToken);
            return true;
        }
    }


}
