using Blogn.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Blogn.Data.EntityTypeConfigurations
{
    public class AccountRoleEntityConfiguration: IEntityTypeConfiguration<AccountRole>
    {
        public void Configure(EntityTypeBuilder<AccountRole> builder)
        {
            builder.HasKey(entity => new {entity.AccountId, entity.Role});
            builder.HasOne(entity => entity.Account).WithMany(entity => entity.Roles);
            builder.Property(entity => entity.Role).IsRequired().HasConversion(new EnumToStringConverter<Role>());
        }
    }
}
