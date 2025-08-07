using Fabulous.MyMeetings.BuildingBlocks.Application.Outbox;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands;
using Fabulous.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals;
using Fabulous.MyMeetings.Modules.Administration.Domain.Members;
using Microsoft.EntityFrameworkCore;

namespace Fabulous.MyMeetings.Modules.Administration.Infrastructure;

public class AdministrationContext(DbContextOptions<AdministrationContext> options) : DbContext(options)
{
    public DbSet<InternalCommandEntity> InternalCommands { get; set; }

    public DbSet<MeetingGroupProposal> MeetingGroupProposals { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    public DbSet<Member> Members { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AdministrationContext).Assembly);
    }
}