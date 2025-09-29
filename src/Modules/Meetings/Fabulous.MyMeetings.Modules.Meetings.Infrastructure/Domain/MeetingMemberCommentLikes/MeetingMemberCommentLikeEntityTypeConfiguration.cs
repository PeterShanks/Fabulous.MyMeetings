using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingMemberCommentLikes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingMemberCommentLikes;

public class MeetingMemberCommentLikeEntityTypeConfiguration: IEntityTypeConfiguration<MeetingMemberCommentLike>
{
    public void Configure(EntityTypeBuilder<MeetingMemberCommentLike> builder)
    {
        builder.ToTable("MeetingMemberCommentLikes", MeetingsContext.DbSchema);

        builder.HasKey(l => l.Id);

        builder.Property(l => l.Id);

        builder.Property(l => l.MeetingCommentId);

        builder.Property(l => l.MemberId);

        builder.Property(l => l.LikeDate);
    }
}