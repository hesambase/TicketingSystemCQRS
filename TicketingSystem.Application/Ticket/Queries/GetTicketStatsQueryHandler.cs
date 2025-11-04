using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Application.Common.Mediator;
using TicketingSystem.Domain.Enums;
using TicketingSystem.Shared.Interfaces;

namespace TicketingSystem.Application.Ticketing.Queries
{
    public class GetTicketStatsQueryHandler : ICommandHandler<GetTicketStatsQuery, Dictionary<TicketStatus, int>>
    {
        private readonly IAppDbContext _db;

        public GetTicketStatsQueryHandler(IAppDbContext db)
        {
            _db = db;
        }

        public async Task<Dictionary<TicketStatus, int>> Handle(GetTicketStatsQuery query, CancellationToken cancellationToken)
        {
            return await _db.Tickets
                .GroupBy(t => t.Status)
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.Status, x => x.Count, cancellationToken);
        }
    }
}
