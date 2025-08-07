using Fabulous.MyMeetings.BuildingBlocks.Infrastructure.InternalCommands;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fabulous.MyMeetings.Modules.UserAccess.Infrastructure.InternalCommands;

internal class InternalCommandEntityTypeConfiguration : IEntityTypeConfiguration<InternalCommandEntity>
{
    public void Configure(EntityTypeBuilder<InternalCommandEntity> builder)
    {
        builder.ToTable("InternalCommands", "Users");

        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id).ValueGeneratedNever();
        builder.Property(b => b.Type);
        builder.Property(b => b.Data);
        builder.Property(b => b.ProcessedDate);
    }
}