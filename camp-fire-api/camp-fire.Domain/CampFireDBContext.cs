using camp_fire.Domain.Entities;
using camp_fire.Domain.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace camp_fire.Domain;

public class CampFireDBContext : DbContext, IDisposable, ICampFireDBContext
{
    public CampFireDBContext(DbContextOptions<CampFireDBContext> dbContextOptions) : base(options: dbContextOptions) { }

    public DbSet<Address> Addresses { get; set; }
    public DbSet<Experience> Experiences { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<EventLog> EventLogs { get; set; }
    public DbSet<Page> Pages { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Scoreboard> Scoreboards { get; set; }
    public DbSet<Story> Stories { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserConfirmation> UserConfirmations { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<Content> Contents { get; set; }
    public DbSet<Box> Boxes { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new AddressEntityTypeConfiguration());
        builder.ApplyConfiguration(new UserEntityTypeConfiguration());
        // builder.ApplyConfiguration(new PageEntityTypeConfiguration());

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseLazyLoadingProxies();
    }
}
