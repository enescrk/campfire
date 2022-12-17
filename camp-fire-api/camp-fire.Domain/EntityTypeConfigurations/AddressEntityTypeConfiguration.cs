using camp_fire.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace camp_fire.Domain.EntityTypeConfigurations
{
    public class AddressEntityTypeConfiguration : BaseEntityTypeConfiguration<Address>
    {
        public override void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Surname).HasMaxLength(50).IsRequired();
            builder.Property(x => x.EMail).HasMaxLength(50).IsRequired();
            builder.Property(x => x.PhoneNumber).HasMaxLength(15);
            // builder.Property(x => x.Password).HasMaxLength(50);
        }
    }
}