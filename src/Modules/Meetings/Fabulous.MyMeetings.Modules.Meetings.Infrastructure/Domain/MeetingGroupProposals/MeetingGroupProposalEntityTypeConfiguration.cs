using Fabulous.MyMeetings.Modules.Meetings.Domain.MeetingGroupProposals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Domain.MeetingGroupProposals;

public class MeetingGroupProposalEntityTypeConfiguration: IEntityTypeConfiguration<MeetingGroupProposal>
{
    public void Configure(EntityTypeBuilder<MeetingGroupProposal> builder)
    {
        builder.ToTable("MeetingGroupProposals", MeetingsContext.DbSchema);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id);

        builder.Property(p => p.Name);

        builder.Property(p => p.Description);

        builder.Property(p => p.ProposalDate);

        builder.Property(p => p.ProposalUserId);

        builder.OwnsOne(p => p.Location, bp =>
        {
            bp.WithOwner();

            bp.Property(x => x.CountryCode)
                .HasColumnName("LocationCountryCode");

            bp.Property(x => x.City)
                .HasColumnName("LocationCity");
        });

        builder.OwnsOne(p => p.Status, pb =>
        {
            pb.WithOwner();
            pb.Property(x => x.Value)
                .HasColumnName("StatusCode");
        });
    }
}