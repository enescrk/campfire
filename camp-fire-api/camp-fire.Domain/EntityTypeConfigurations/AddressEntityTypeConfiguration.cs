using camp_fire.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace camp_fire.Domain.EntityTypeConfigurations
{
    public class AddressEntityTypeConfiguration : BaseEntityTypeConfiguration<Address>
    {
        public override void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(x => x.CountryId).IsRequired();
            builder.Property(x => x.City).HasMaxLength(100).IsRequired();
            builder.Property(x => x.OpenAddress).HasMaxLength(500).IsRequired();
        }
    }
}