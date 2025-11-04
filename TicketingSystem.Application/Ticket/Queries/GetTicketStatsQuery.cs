using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Application.Common.Mediator;
using TicketingSystem.Domain.Enums;

namespace TicketingSystem.Application.Ticketing.Queries
{
    public class GetTicketStatsQuery : ICommand<Dictionary<TicketStatus, int>> { }
}
