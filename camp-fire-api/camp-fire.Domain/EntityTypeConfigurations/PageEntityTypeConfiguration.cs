using camp_fire.Domain.Entities;
using camp_fire.Domain.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PageEntityTypeConfiguration : BaseEntityTypeConfiguration<Page>
{
    public override void Configure(EntityTypeBuilder<Page> builder)
    {
        builder.HasOne(x => x.Scoreboard)
               .WithOne(y => y.Page)
               .HasForeignKey<Scoreboard>(z => z.PageId);
    }
}
