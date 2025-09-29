using Fabulous.MyMeetings.Modules.Meetings.Domain.MemberSubscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Domain.MemberSubscriptions;

public class MemberSubscriptionEntityTypeConfiguration: IEntityTypeConfiguration<MemberSubscription>
{
    public void Configure(EntityTypeBuilder<MemberSubscription> builder)
    {
        builder.ToTable("MemberSubscriptions", MeetingsContext.DbSchema);

        builder.HasKey(ms => ms.Id);

        builder.Property(ms => ms.Id);

        builder.Property(m => m.ExpirationDate);
    }
}