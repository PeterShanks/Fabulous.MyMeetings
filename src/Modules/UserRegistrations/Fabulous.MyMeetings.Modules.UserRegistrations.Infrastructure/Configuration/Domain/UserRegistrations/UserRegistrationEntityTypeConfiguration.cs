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

        builder.ComplexProperty(p => p.Status, b =>
        {
            b.Property(p => p.Value)
                .HasColumnName("StatusCode");
        });
    }
}