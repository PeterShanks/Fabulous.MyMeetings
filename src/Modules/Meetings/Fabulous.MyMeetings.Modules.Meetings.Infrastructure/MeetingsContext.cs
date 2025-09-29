using Fabulous.MyMeetings.BuildingBlocks.Application.Outbox;
using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;
using Fabulous.MyMeetings.Modules.Meetings.Domain.MemberSubscriptions;
using Microsoft.EntityFrameworkCore;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure;

public class MeetingsContext(DbContextOptions<MeetingsContext> options) : DbContext(options)
{
    public const string DbSchema = "Meetings";
    public DbSet<MeetingGroup> MeetingGroups { get; set; }
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<MeetingGroupProposal> MeetingGroupProposals { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    public DbSet<InternalCommandEntity> InternalCommands { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<MemberSubscription> MemberSubscriptions { get; set; }
    public DbSet<MeetingComment> MeetingComments { get; set; }
    public DbSet<MeetingCommentingConfiguration> MeetingCommentingConfigurations { get; set; }
    public DbSet<MeetingMemberCommentLike> MeetingMemberCommentLikes { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MeetingsContext).Assembly);
    }
}