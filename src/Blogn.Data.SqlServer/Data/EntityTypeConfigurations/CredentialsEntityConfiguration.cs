using Blogn.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blogn.Data.EntityTypeConfigurations
{
    public class CredentialsEntityConfiguration: IEntityTypeConfiguration<Credentials>
    {
        public void Configure(EntityTypeBuilder<Credentials> builder)
        {
            builder.HasKey(entity => entity.AccountId).IsClustered().HasName("PK_Credentials");
            builder.Property(entity => entity.Password).IsRequired().HasColumnType("VARCHAR(256)");
            builder.Property(entity => entity.DateCreated).IsRequired();
            builder.Property(entity => entity.DateUpdated).IsRequired();
            builder.HasOne(entity => entity.Account).WithOne(entity => entity.Credentials);
            builder.HasMany(entity => entity.ResetTokens).WithOne(entity => entity.Credentials);
        }
    }
}
