using Fabulous.MyMeetings.BuildingBlocks.Application.Outbox;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands;
using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations;
using Microsoft.EntityFrameworkCore;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure
{
    internal class RegistrationsContext(DbContextOptions<RegistrationsContext> options) : DbContext(options)
    {
        public DbSet<UserRegistration> UserRegistrations { get; init; }

        public DbSet<OutboxMessage> OutboxMessages { get; init; }

        public DbSet<InternalCommand> InternalCommands { get; init; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RegistrationsContext).Assembly);
        }
    }
}
