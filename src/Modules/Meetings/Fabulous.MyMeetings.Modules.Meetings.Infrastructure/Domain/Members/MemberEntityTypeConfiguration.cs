using Fabulous.MyMeetings.Modules.Meetings.Domain.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Domain.Members;

public class MemberEntityTypeConfiguration: IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("Members", MeetingsContext.DbSchema);

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id);

        builder.Property(m => m.Email);

        builder.Property(m => m.FirstName);

        builder.Property(m => m.LastName);

        builder.Property(m => m.Name);

        builder.Property(m => m.CreatedAt);
    }
}