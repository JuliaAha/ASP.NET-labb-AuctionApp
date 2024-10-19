using Microsoft.EntityFrameworkCore;

namespace AuctionApplication.Persistence;

public class AuctionDbContext : DbContext
{
    public AuctionDbContext(DbContextOptions<AuctionDbContext> options) : base(options) { }
    
    public DbSet<BidDb> BidDbs { get; set; }
    public DbSet<AuctionDb> AuctionDbs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) //seed data
    {
        AuctionDb adb = new AuctionDb()
        {
            Id = -1, //seed date
            Title = "Kofta",
            Description = "Hej",
            EndDate = DateTime.Now.AddDays(3),
            AuctionOwner = "julg@kth.se",
            StartingPrice = 200,
            BidDbs = new List<BidDb>()
        };
        modelBuilder.Entity<AuctionDb>().HasData(adb);

        BidDb bdb1 = new BidDb()
        {
            Id = -1,
            UserName = "emma@kth.se",
            BidDate = DateTime.Now,
            Amount = 250,
            AuctionId =-1,
        };
        BidDb bdb2 = new BidDb()
        {
            Id = -2,
            UserName = "emma@kth.se",
            BidDate = DateTime.Now,
            Amount = 300,
            AuctionId =-1,
        };
        modelBuilder.Entity<BidDb>().HasData(bdb1);
        modelBuilder.Entity<BidDb>().HasData(bdb2);
    }
}