using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingCommentingConfigurations;
using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingCommentingConfigurations;

public class MeetingCommentingConfigurationEntityTypeConfiguration: IEntityTypeConfiguration<MeetingCommentingConfiguration>
{
    public void Configure(EntityTypeBuilder<MeetingCommentingConfiguration> builder)
    {
        builder.ToTable("MeetingCommentingConfigurations", MeetingsContext.DbSchema);

        builder.HasKey(x => x.Id);

        builder.Property(p => p.Id)
            .HasColumnName("Id")
            .IsRequired();

        builder.Property(p => p.MeetingId)
            .HasColumnName("MeetingId")
            .IsRequired();

        builder.Property(p => p.IsCommentingEnabled)
            .HasColumnName("IsCommentingEnabled")
            .IsRequired();

        builder.HasOne<Meeting>()
            .WithOne()
            .HasForeignKey<MeetingCommentingConfiguration>(x => x.MeetingId);
    }
}