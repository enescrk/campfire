using camp_fire.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace camp_fire.Domain.EntityTypeConfigurations;

public class UserEntityTypeConfiguration : BaseEntityTypeConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        // builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
        // builder.Property(x => x.Surname).HasMaxLength(50).IsRequired();
        builder.Property(x => x.EMail).HasMaxLength(50).IsRequired();
        builder.Property(x => x.PhoneNumber).HasMaxLength(15);
        // builder.HasOne(x => x.Address)
        //        .WithOne(y => y.User)
        //        .HasForeignKey<Address>(z => z.UserId);
    }
}
