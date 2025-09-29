using Fabulous.MyMeetings.Modules.Administration.Domain.Members;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fabulous.MyMeetings.Modules.Administration.Infrastructure.Domain.Members;

public class MemberEntityTypeConfiguration: IEntityTypeConfiguration<Member>
{
    public void Configure(EntityTypeBuilder<Member> builder)
    {
        builder.ToTable("Members", DbSchema.Administration);

        builder.HasKey(b => b.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever();

        builder.Property(p => p.Email);
        builder.Property(p => p.Name);
        builder.Property(p => p.FirstName);
        builder.Property(p => p.LastName);
        builder.Property(p => p.CreatedDate);
    }
}