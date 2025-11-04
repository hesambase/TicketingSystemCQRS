using System.Data;
using TicketingSystem.Domain.Enums;

public class User
{

    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public Role Role { get; set; }

    // Constructor کامل
    public User(string fullName, string email, string passwordHash, Role role)
    {
        Id = Guid.NewGuid();
        FullName = fullName;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }

    // Constructor پیش‌فرض برای initializer
    public User() { }




}