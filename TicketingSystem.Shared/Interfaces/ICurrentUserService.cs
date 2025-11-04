using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketingSystem.Shared.Interfaces
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        string Email { get; }
        string Role { get; }
        bool IsAuthenticated { get; }
    }

}
