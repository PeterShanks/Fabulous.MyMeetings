using Fabulous.MyMeetings.BuildingBlocks.Application.Outbox;
using Fabulous.MyMeetings.Modules.Registrations.Domain.UserRegistrations;
using Microsoft.EntityFrameworkCore;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands;

namespace Fabulous.MyMeetings.Modules.Registrations.Infrastructure
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
