using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TicketingSystem.Shared.Interfaces;




    namespace TicketingSystem.Infrastructure.Auth
    {
        public class CurrentUserService : ICurrentUserService
        {
            private readonly IHttpContextAccessor _httpContextAccessor;

            public CurrentUserService(IHttpContextAccessor httpContextAccessor)
            {
                _httpContextAccessor = httpContextAccessor;
            }

            public Guid UserId =>
                Guid.TryParse(_httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier).Value, out var id)
                    ? id
                    : Guid.Empty;

            public string Email =>
                _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email).Value ?? string.Empty;

            public string Role =>
                _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Role).Value ?? string.Empty;

            public bool IsAuthenticated =>
                _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        }
    }

