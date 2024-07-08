using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.Modules.UserAccess.Domain.UserRegistrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Domain.UserRegistrations;

internal class UserRegistrationEntityTypeConfiguration : IEntityTypeConfiguration<UserRegistration>
{
    public void Configure(EntityTypeBuilder<UserRegistration> builder)
    {
        builder.ToTable("UserRegistrations", "Users");
        builder.HasKey(t => t.Id);

        builder.Property(p => p.Id)
            .HasConversion<TypedIdValueConverter<UserRegistrationId>>()
            .ValueGeneratedNever();

        builder.Property(p => p.Login)
            .HasColumnName("Login");

        builder.Property(p => p.Email)
            .HasColumnName("Email");

        builder.Property(p => p.Password)
            .HasColumnName("Password");

        builder.Property(t => t.FirstName)
            .HasColumnName("FirstName");

        builder.Property(t => t.LastName)
            .HasColumnName("LastName");

        builder.Property(p => p.Name)
            .HasColumnName("Name");

        builder.Property(p => p.RegisterDate)
            .HasColumnName("RegisterDate");

        builder.Property(p => p.ConfirmedDate)
            .HasColumnName("ConfirmedDate");

        builder.ComplexProperty(p => p.Status, b =>
        {
            b.Property(p => p.Value)
                .HasColumnName("StatusCode");
        });
    }
}