using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Application.Common.Mediator;
using TicketingSystem.Shared.Interfaces;

namespace TicketingSystem.Application.Auth.Commands
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand, string>
    {
        private readonly IAppDbContext _db;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(IAppDbContext db, ITokenService tokenService)
        {
            _db = db;
            _tokenService = tokenService;
        }

        public async Task<string> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == command.Email, cancellationToken);

            //if (user is null || user.PasswordHash != HashPassword(command.Password))
              //throw new UnauthorizedAccessException("Invalid credentials");

            return _tokenService.GenerateToken(user);
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
