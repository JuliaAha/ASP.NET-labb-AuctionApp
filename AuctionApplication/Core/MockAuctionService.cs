using AuctionApplication.Core.Interfaces;
using AuctionApplication.Core.Interfaces.Interfaces;

namespace AuctionApplication.Core;

public class MockAuctionService : IAuctionService
{
    public List<Auction> GetAllByUserName(string userName)
    {
        return _auctions;
    }

    public List<Auction> GetAllActive()
    {
        List<Auction> activeAuctions = new List<Auction>();
        foreach (Auction auction in _auctions)
        {
            if (auction.IsActive())
            {
                activeAuctions.Add(auction);
            } 
        }
        activeAuctions.Sort();
        return activeAuctions;
    }

    public Auction GetById(int id)
    {
        return _auctions.Find(a => a.AuctionId == id && a.IsActive());
    }

    public void Add(string userName, string Title)
    {
        throw new NotImplementedException("MockProjectService.Add");
    }
    private static readonly List<Auction> _auctions = new();
    
    //C# style static initializer
    static MockAuctionService()
    {
        Auction a1 = new Auction(1,"Katt","julg@kth.se", "En fin katt", DateTime.Today.AddDays(1));
        Auction a2 = new Auction(2,"Kattt","julg@kth.se", "En ful katt", DateTime.Today.AddDays(2));
        Auction a3 = new Auction(3,"Katttt","julg@kth.se", "En svart katt", DateTime.Today.AddDays(3));
        Auction a4 = new Auction(4,"Kattttt","julg@kth.se", "En vit katt", DateTime.Today.AddDays(-1));
        
        a2.AddBid(new Bids(1, "julg@kth.se", 100));
        a2.AddBid(new Bids(1, "julg@kth.se", 150));
        _auctions.Add(a1);
        _auctions.Add(a2);
        _auctions.Add(a3);
        _auctions.Add(a4);
    }
}