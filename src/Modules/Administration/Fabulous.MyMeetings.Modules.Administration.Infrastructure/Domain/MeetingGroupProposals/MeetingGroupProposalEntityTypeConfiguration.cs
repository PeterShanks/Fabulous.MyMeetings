using Fabulous.MyMeetings.Modules.Administration.Domain.MeetingGroupProposals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fabulous.MyMeetings.Modules.Administration.Infrastructure.Domain.MeetingGroupProposals;

public class MeetingGroupProposalEntityTypeConfiguration: IEntityTypeConfiguration<MeetingGroupProposal>
{
    public void Configure(EntityTypeBuilder<MeetingGroupProposal> builder)
    {
        builder.ToTable("MeetingGroupProposals", DbSchema.Administration);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever();

        builder.Property(p => p.Name);
        builder.Property(p => p.Description);

        builder.ComplexProperty(p => p.Location, b =>
        {
            b.Property(l => l.City)
                .HasColumnName("LocationCity");

            b.Property(l => l.CountryCode)
                .HasColumnName("LocationCountryCode");
        });

        builder.Property(p => p.ProposalDate);

        builder.Property(p => p.ProposalUserId);

        builder.ComplexProperty(p => p.Status, b =>
        {
            b.Property(s => s.Value)
                .HasColumnName("StatusCode");
        });

        builder.ComplexProperty(p => p.Decision, b =>
        {
            b.Property(d => d.Date)
                .HasColumnName("DecisionDate");
            b.Property(d => d.UserId);
            b.Property(d => d.Code)
                .HasColumnName("DecisionCode");
            b.Property(d => d.RejectReason)
                .HasColumnName("DecisionRejectReason");
        });
    }
}