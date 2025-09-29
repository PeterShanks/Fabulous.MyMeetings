using Fabulous.MyMeetings.Modules.UserAccess.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.Domain.Users;

internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", "Users");

        builder.HasKey(x => x.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever();

        builder.Property(p => p.Email)
            .HasColumnName("Email");

        builder.Property(p => p.Password)
            .HasColumnName("Password");

        builder.Property(p => p.IsActive)
            .HasColumnName("IsActive");

        builder.Property(t => t.FirstName)
            .HasColumnName("FirstName");

        builder.Property(t => t.LastName)
            .HasColumnName("LastName");

        builder.Property(p => p.Name)
            .HasColumnName("Name");

        builder.OwnsMany(p => p.Roles, b =>
        {
            b.WithOwner()
                .HasForeignKey("UserId");

            b.ToTable("UserRoles", "Users");
            b.Property<UserId>("UserId");
            b.Property(p => p.Value)
                .HasColumnName("RoleCode");
            b.HasKey("UserId", nameof(UserRole.Value));
        });
    }
}