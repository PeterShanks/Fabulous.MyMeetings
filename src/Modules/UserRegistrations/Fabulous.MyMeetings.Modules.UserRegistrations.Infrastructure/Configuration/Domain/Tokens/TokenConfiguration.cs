using Fabulous.MyMeetings.Modules.UserRegistrations.Domain.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration.Domain.Tokens;

public class TokenConfiguration: IEntityTypeConfiguration<Token>
{
    public void Configure(EntityTypeBuilder<Token> builder)
    {
        builder.ToTable("Tokens", DatabaseSchema.UserRegistrations);
        builder.HasKey(t => t.Id);

        builder.Property(p => p.UserId);
        builder.Property(p => p.Value);
        builder.Property(p => p.TokenTypeId);
        builder.Property(p => p.CreatedAt);
        builder.Property(p => p.ExpiresAt);
        builder.Property(p => p.IsUsed);
        builder.Property(p => p.UsedAt);
        builder.Property(p => p.IsInvalidated);
        builder.Property(p => p.InvalidatedAt);

        builder.Property(t => t.Id)
            .ValueGeneratedNever();
    }
}