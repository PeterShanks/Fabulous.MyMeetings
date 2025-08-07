using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingGroups;

public class MeetingGroupEntityTypeConfiguration: IEntityTypeConfiguration<MeetingGroup>
{
    public void Configure(EntityTypeBuilder<MeetingGroup> builder)
    {
        builder.ToTable("MeetingGroups", MeetingsContext.DbSchema);

        builder.HasKey(g => g.Id);

        builder.Property(p => p.Id);

        builder.Property(p => p.Name);

        builder.Property(p => p.Description);

        builder.OwnsOne(p => p.Location, b =>
        {
            b.WithOwner();

            b.Property(x => x.CountryCode)
                .HasColumnName("LocationCountryCode");

            b.Property(x => x.City)
                .HasColumnName("LocationCity");
        });

        builder.Property(p => p.CreatorId);

        builder.OwnsMany(p => p.Members, b =>
        {
            b.ToTable("MeetingGroupMembers", MeetingsContext.DbSchema);
            b.WithOwner()
                .HasForeignKey("MeetingGroupId");

            b.HasKey(x => new { x.MemberId, x.MeetingGroupId, x.JoinedDate });

            b.Property(x => x.MemberId);

            b.Property(x => x.MeetingGroupId);

            b.OwnsOne(x => x.Role, pb =>
            {
                pb.WithOwner();
                pb.Property(x => x.Value)
                    .HasColumnName("RoleCode");
            });

            b.Property(x => x.IsActive);

            b.Property(p => p.JoinedDate);

            b.Property(p => p.LeaveDate);
        });
    }
}