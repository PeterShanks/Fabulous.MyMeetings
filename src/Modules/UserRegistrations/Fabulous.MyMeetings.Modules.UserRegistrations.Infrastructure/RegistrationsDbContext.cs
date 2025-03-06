using Fabulous.MyMeetings.BuildingBlocks.Application.Outbox;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands;
using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.Tokens;
using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure
{
    internal class UserRegistrationsContext(DbContextOptions<UserRegistrationsContext> options) : DbContext(options), IDataProtectionKeyContext
    {
        public DbSet<UserRegistration> UserRegistrations { get; init; }

        public DbSet<OutboxMessage> OutboxMessages { get; init; }

        public DbSet<InternalCommand> InternalCommands { get; init; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserRegistrationsContext).Assembly);
            modelBuilder.Entity<DataProtectionKey>().ToTable("DataProtectionKeys", "App");
        }
    }
}
