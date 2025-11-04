using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Application.Common.Mediator;
using TicketingSystem.Domain.Enums;

namespace TicketingSystem.Application.Ticketing.Commands
{
    public class CreateTicketCommand : ICommand<Guid>
    {
        public TicketPriority Priority { get; set; } = TicketPriority.Medium;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid CreatedByUserId { get; set; }
    }

}
