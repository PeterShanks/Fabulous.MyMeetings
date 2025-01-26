using Fabulous.MyMeetings.BuildingBlocks.Infrastructure;
using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.UserRegistrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Domain.UserRegistrations
{
    internal class UserRegistrationEntityConfiguration : IEntityTypeConfiguration<UserRegistration>
    {
        public void Configure(EntityTypeBuilder<UserRegistration> builder)
        {
            builder.ToTable("UserRegistrations", "Registrations");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Id)
                .HasConversion<TypedIdValueConverter<UserRegistrationId>>()
                .ValueGeneratedNever();

            builder.OwnsOne(p => p.Status)
                .Property(x => x.Value)
                .HasColumnName("StatusCode");
        }
    }
}
