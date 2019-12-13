using Blogn.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blogn.Data.EntityTypeConfigurations
{
    public class AccountEntityConfiguration: IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(entity => entity.Id).IsClustered().HasName("PK_Account");
            builder.Property(entity => entity.Id).UseIdentityColumn();
            builder.Property(entity => entity.Email).IsRequired().HasMaxLength(384);
            builder.HasIndex(entity => entity.Email).IsUnique().HasName("UK_Account_Email");
            builder.Property(entity => entity.DisplayName).IsRequired().HasMaxLength(64);
            builder.Property(entity => entity.AvatarId).IsRequired().HasColumnType("VARCHAR(32)");
            builder.Property(entity => entity.IsEnabled).IsRequired();
            builder.HasMany(entity => entity.Roles).WithOne(entity => entity.Account);
        }
    }
}
