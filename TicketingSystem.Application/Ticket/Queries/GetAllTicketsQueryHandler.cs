using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Application.Common.Mediator;
using TicketingSystem.Application.Ticketing.Dtos;
using TicketingSystem.Shared.Interfaces;

namespace TicketingSystem.Application.Ticketing.Queries
{
    public class GetAllTicketsQueryHandler : ICommandHandler<GetAllTicketsQuery, List<TicketDto>>
    {
        private readonly IAppDbContext _db;

        public GetAllTicketsQueryHandler(IAppDbContext db)
        {
            _db = db;
        }

        public async Task<List<TicketDto>> Handle(GetAllTicketsQuery query, CancellationToken cancellationToken)
        {
            return await _db.Tickets
                .OrderByDescending(t => t.CreatedAt)
                .Select(t => new TicketDto
                {
                    Id = t.Id,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status,
                    Priority = t.Priority,
                    CreatedByUserId = t.CreatedByUserId,
                    AssignedToUserId = t.AssignedToUserId,
                    CreatedAt = t.CreatedAt
                })
                .ToListAsync(cancellationToken);
        }
    }
}
