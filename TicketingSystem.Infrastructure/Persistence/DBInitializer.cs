using global::TicketingSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using TicketingSystem.Domain.Enums;

namespace TicketingSystem.Infrastructure.Persistence
{


    public static class DbInitializer
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // فقط اگر دیتابیس خالی باشه، کاربرهای اولیه ساخته می‌شن
            if (!context.Users.Any())
            {
                var admin = new User(
                    fullName: "Admin User",
                    email: "admin@example.com",
                    passwordHash: BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                    role: Role.Admin
                );

                var employee = new User(
                    fullName: "Employee User",
                    email: "employee@example.com",
                    passwordHash: BCrypt.Net.BCrypt.HashPassword("Employee123!"),
                    role: Role.Employee
                );

                context.Users.AddRange(admin, employee);
                await context.SaveChangesAsync();
            }
        }


    }
}
