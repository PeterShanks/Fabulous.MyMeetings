using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingComments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingComments;

public class MeetingCommentEntityTypeConfiguration: IEntityTypeConfiguration<MeetingComment>
{
    public void Configure(EntityTypeBuilder<MeetingComment> builder)
    {
        builder.ToTable("MeetingComments", MeetingsContext.DbSchema);

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id);

        builder.Property(p => p.MeetingId);

        builder.Property(p => p.AuthorId);

        builder.Property(p => p.InReplyToCommentId);

        builder.Property(p => p.Comment);

        builder.Property(p => p.CreateDate);

        builder.Property(p => p.EditDate);

        builder.Property(p => p.IsRemoved);

        builder.Property(p => p.RemovedByReason);
    }
}