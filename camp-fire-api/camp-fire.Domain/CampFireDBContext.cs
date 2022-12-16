using camp_fire.Domain.Entities;
using camp_fire.Domain.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;
namespace camp_fire.Domain;

public class CampFireDBContext : DbContext, IDisposable, ICampFireDBContext
{
    public CampFireDBContext(DbContextOptions<CampFireDBContext> dbContextOptions) : base(options: dbContextOptions) { }

    public DbSet<Event> Events { get; set; }
    public DbSet<EventLog> EventLogs { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<County> Counties { get; set; }
    public DbSet<Page> Pages { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Scoreboard> Scoreboards { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserEntityTypeConfiguration());

        base.OnModelCreating(builder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseLazyLoadingProxies();
    }
}
