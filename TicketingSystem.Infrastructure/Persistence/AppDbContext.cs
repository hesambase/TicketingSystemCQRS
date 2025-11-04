using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using global::TicketingSystem.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;


    namespace TicketingSystem.Infrastructure.Persistence
    {
        public class AppDbContext : DbContext, IAppDbContext
        {
            public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

            public DbSet<User> Users => Set<User>();
            public DbSet<Ticket> Tickets => Set<Ticket>();

            public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            {
                return await base.SaveChangesAsync(cancellationToken);
            }

            protected override void OnModelCreating(ModelBuilder builder)
            {
                base.OnModelCreating(builder);

                // تنظیمات User
                builder.Entity<User>(entity =>
                {
                    entity.HasKey(u => u.Id);
                    entity.Property(u => u.FullName).IsRequired().HasMaxLength(150);
                    entity.Property(u => u.Email).IsRequired();
                    entity.HasIndex(u => u.Email).IsUnique();
                });

                // تنظیمات Ticket
                builder.Entity<Ticket>(entity =>
                {
                    entity.HasKey(t => t.Id);
                    entity.Property(t => t.Title).IsRequired().HasMaxLength(150);
                    entity.Property(t => t.Description).IsRequired().HasMaxLength(1000);
                });
            }
        }
    }

