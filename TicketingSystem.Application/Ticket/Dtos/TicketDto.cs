using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Domain.Enums;

namespace TicketingSystem.Application.Ticketing.Dtos
{
    public class TicketDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TicketStatus Status { get; set; }
        public TicketPriority Priority { get; set; } 
        public Guid CreatedByUserId { get; set; }
        public Guid? AssignedToUserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
