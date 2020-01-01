using Blogn.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blogn.Data.EntityTypeConfigurations
{
    public class ResetTokenEntityConfiguration: IEntityTypeConfiguration<ResetToken>
    {
        public void Configure(EntityTypeBuilder<ResetToken> builder)
        {
            builder.HasKey(entity => entity.Id).IsClustered().HasName("PK_ResetToken");
            builder.Property(entity => entity.Id).UseIdentityColumn();
            builder.Property(entity => entity.Token).IsRequired().HasColumnType("VARCHAR(36)");
            builder.HasIndex(entity => entity.Token).IsUnique().HasName("UK_ResetToken_Token");
            builder.Property(entity => entity.DateCreated).IsRequired();
            builder.Property(entity => entity.DateExpired).IsRequired();
            builder.HasOne(entity => entity.Credentials).WithMany(entity => entity.ResetTokens);
        }
    }
}
