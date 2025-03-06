using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration.Domain.UserRegistrations;

internal class UserRegistrationEntityTypeConfiguration : IEntityTypeConfiguration<UserRegistration>
{
    public void Configure(EntityTypeBuilder<UserRegistration> builder)
    {
        builder.ToTable("UserRegistrations", DatabaseSchema.UserRegistrations);
        builder.HasKey(t => t.Id);

        builder.Property(p => p.Id)
            .HasConversion<TypedIdValueConverter<UserRegistrationId>>()
            .ValueGeneratedNever();

        builder.Property(p => p.Password);
        builder.Property(p => p.Email);
        builder.Property(p => p.FirstName);
        builder.Property(p => p.LastName);
        builder.Property(p => p.Name);
        builder.Property(p => p.RegisterDate);
        builder.Property(p => p.ConfirmedDate);

        builder.ComplexProperty(p => p.Status, b =>
        {
            b.Property(p => p.Value)
                .HasColumnName("StatusCode");
        });
    }
}