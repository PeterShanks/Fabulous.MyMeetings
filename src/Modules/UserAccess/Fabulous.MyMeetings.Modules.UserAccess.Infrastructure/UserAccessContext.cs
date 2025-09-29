using Fabulous.MyMeetings.BuildingBlocks.Application.Outbox;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands;
using Fabulous.MyMeetings.Modules.UserAccess.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure;

internal class UserAccessContext(DbContextOptions<UserAccessContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; init; }

    public DbSet<OutboxMessage> OutboxMessages { get; init; }

    public DbSet<InternalCommandEntity> InternalCommands { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserAccessContext).Assembly);
    }
}