using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystem.Shared.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Ticket> Tickets { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
