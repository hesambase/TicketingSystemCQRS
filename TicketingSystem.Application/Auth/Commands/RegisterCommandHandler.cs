using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Application.Common.Mediator;
using TicketingSystem.Domain.Enums;
using TicketingSystem.Shared.Interfaces;

namespace TicketingSystem.Application.Auth.Commands
{
    public class RegisterCommandHandler : ICommandHandler<RegisterCommand, Guid>
    {
        private readonly IAppDbContext _db;

        public RegisterCommandHandler(IAppDbContext db)
        {
            _db = db;
        }

        public async Task<Guid> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            var exists = await _db.Users.AnyAsync(u => u.Email == command.Email, cancellationToken);
            if (exists)
                throw new InvalidOperationException("Email already registered");
           

            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = command.FullName,
                Email = command.Email,
                PasswordHash = HashPassword(command.Password),
                Role = Role.Employee
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync(cancellationToken);

            return user.Id;
        }

        private string HashPassword(string password)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

    }
}
