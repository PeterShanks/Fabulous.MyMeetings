using Fabulous.MyMeetings.Modules.Meetings.Domain.Meetings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fabulous.MyMeetings.Modules.Meetings.Infrastructure.Domain.Meetings;

public class MeetingEntityTypeConfiguration: IEntityTypeConfiguration<Meeting>
{
    public void Configure(EntityTypeBuilder<Meeting> builder)
    {
        builder.ToTable("Meetings", MeetingsContext.DbSchema);

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id);

        builder.Property(m => m.MeetingGroupId);

        builder.Property(m => m.Title);

        builder.OwnsOne(m => m.Term, tb =>
        {
            tb.WithOwner();

            tb.Property(m => m.StartDate)
                .HasColumnName("TermStartDate");

            tb.Property(m => m.EndDate)
                .HasColumnName("TermEndDate");
        });

        builder.Property(m => m.Description);

        builder.OwnsOne(m => m.Location, lb =>
        {
            lb.WithOwner();

            lb.Property(m => m.Name)
                .HasColumnName("LocationName");

            lb.Property(m => m.Address)
                .HasColumnName("LocationAddress");

            lb.Property(m => m.PostalCode)
                .HasColumnName("LocationPostalCode");

            lb.Property(m => m.City)
                .HasColumnName("LocationCity");
        });

        builder.OwnsOne(m => m.MeetingLimits, mb =>
        {
            mb.WithOwner();

            mb.Property(m => m.AttendeesLimit)
                .HasColumnName("AttendeesLimit");

            mb.Property(m => m.GuestsLimit)
                .HasColumnName("GuestsLimit");
        });

        builder.OwnsOne(m => m.RsvpTerm, b =>
        {
            b.WithOwner();

            b.Property(m => m.StartDate)
                .HasColumnName("RSVPTermStartDate");

            b.Property(m => m.EndDate)
                .HasColumnName("RSVPTermEndDate");
        });

        builder.OwnsOne(m => m.EventFee, b =>
        {
            b.WithOwner();

            b.Property(m => m.Value)
                .HasColumnName("EventFeeValue");

            b.Property(m => m.Currency)
                .HasColumnName("EventFeeCurrency");
        });

        builder.Property(m => m.CreatorId);

        builder.Property(m => m.CreatedDate);

        builder.Property(m => m.ChangeMemberId);

        builder.Property(m => m.ChangeDate);

        builder.Property(m => m.CancelDate);

        builder.Property(m => m.CancelMemberId);

        builder.Property(m => m.IsCanceled);

        builder.OwnsMany(m => m.Attendees, b =>
        {
            b.WithOwner()
                .HasForeignKey("MeetingId");
            b.ToTable("MeetingAttendees", MeetingsContext.DbSchema);

            b.HasKey(m => new { m.AttendeeId, m.MeetingId, m.DecisionDate });

            b.Property(m => m.AttendeeId);

            b.Property(m => m.MeetingId);

            b.Property(m => m.DecisionDate);

            b.OwnsOne(m => m.Role, rb =>
            {
                rb.WithOwner();

                rb.Property(m => m.Value)
                    .HasColumnName("RoleCode");
            });

            b.Property(m => m.GuestsNumber);

            b.Property(m => m.DecisionChanged);

            b.Property(m => m.DecisionChangedDate);

            b.Property(m => m.RemovedDate);

            b.Property(m => m.RemovingMemberId);

            b.Property(m => m.RemovingReason);

            b.Property(m => m.IsRemoved);

            b.OwnsOne(m => m.Fee, fb =>
            {
                fb.WithOwner();
                fb.Property(m => m.Value)
                    .HasColumnName("FeeValue");
                fb.Property(m => m.Currency)
                    .HasColumnName("FeeCurrency");
            });

            b.Property(m => m.IsFeedPaid);
        });

        builder.OwnsMany(p => p.NotAttendees, b =>
        {
            b.WithOwner()
                .HasForeignKey("MeetingId");

            b.ToTable("MeetingNotAttendees", MeetingsContext.DbSchema);

            b.HasKey(m => new { m.MemberId, m.MeetingId, m.DecisionDate });

            b.Property(m => m.MemberId);

            b.Property(m => m.MeetingId);

            b.Property(m => m.DecisionDate);

            b.Property(m => m.DecisionChanged);

            b.Property(m => m.DecisionChangeDate);
        });

        builder.OwnsMany(p => p.WaitlistMembers, b =>
        {
            b.WithOwner()
                .HasForeignKey("MeetingId");

            b.ToTable("MeetingWaitlistMembers", MeetingsContext.DbSchema);

            b.HasKey(m => new { m.MemberId, m.MeetingId, m.SignOffDate });

            b.Property(m => m.MemberId);

            b.Property(m => m.MeetingId);

            b.Property(m => m.SignUpDate);

            b.Property(m => m.SignOffDate);

            b.Property(m => m.IsSignedOff);

            b.Property(m => m.IsMovedToAttendees);

            b.Property(m => m.MovedToAttendeesDate);
        });
    }
}