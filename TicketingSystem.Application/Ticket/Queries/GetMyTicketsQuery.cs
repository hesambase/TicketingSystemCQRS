using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Application.Common.Mediator;

using TicketingSystem.Application.Ticketing.Dtos;

namespace TicketingSystem.Application.Ticketing.Queries
{
    public class GetMyTicketsQuery : ICommand<List<TicketDto>>
    {
        public Guid UserId { get; set; }
    }
}
