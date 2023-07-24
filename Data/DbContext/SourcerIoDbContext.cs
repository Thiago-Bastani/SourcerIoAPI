
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using SorcerIo.Domain;

namespace SorcerIoAPI.Data;

public partial class SourcerIoDbContext : DbContext
{
    private IConfiguration _config;
    public SourcerIoDbContext(IConfiguration config)
    {
        _config = config;
    }

    public SourcerIoDbContext(DbContextOptions<SourcerIoDbContext> options, IConfiguration config)
        : base(options)
    {
        _config = config;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(_config.GetConnectionString("SQLEXPRESS") ?? throw new Exception("DB String not found."));

    public DbSet<Player> Players { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<PlayerAttributes> PlayerAttributes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Player>()
            .HasOne(player => player.Attributes)
            .WithOne(pattr => pattr.Player)
            .HasForeignKey<PlayerAttributes>(pattr => pattr.Id);

        modelBuilder.Entity<User>()
            .HasMany(user => user.Player)
            .WithOne(player => player.User);

    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
