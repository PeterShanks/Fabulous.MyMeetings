using Fabulous.MyMeetings.BuildingBlocks.Domain.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fabulous.MyMeetings.Modules.UserRegistrations.Infrastructure.Configuration.Domain.Tokens
{
    public class TokenConfiguration: IEntityTypeConfiguration<Token>
    {
        public void Configure(EntityTypeBuilder<Token> builder)
        {
            builder.ToTable("Tokens", DatabaseSchema.App);
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .ValueGeneratedNever();
        }
    }
}
