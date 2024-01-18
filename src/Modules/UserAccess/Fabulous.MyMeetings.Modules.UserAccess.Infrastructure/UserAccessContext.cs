using Fabulous.MyMeetings.BuildingBlocks.Application.Outbox;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands;
using Fabulous.MyMeetings.Modules.UserAccess.Domain.UserRegistrations;
using Fabulous.MyMeetings.Modules.UserAccess.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure
{
    internal class UserAccessContext : DbContext
    {
        public DbSet<UserRegistration> UserRegistrations { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public DbSet<InternalCommand> InternalCommands { get; set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public UserAccessContext(DbContextOptions<UserAccessContext> options) : base(options)
        {
            
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserAccessContext).Assembly);
        }
    }
}
