using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Application.Common.Mediator;
using TicketingSystem.Domain.Enums;

namespace TicketingSystem.Application.Ticketing.Commands
{
    public class UpdateTicketCommand : ICommand<bool>
    {
        public TicketPriority? Priority { get; set; }
        public Guid TicketId { get; set; }
        public Guid? AssignedToUserId { get; set; }
        public TicketStatus Status { get; set; }
    }
}
