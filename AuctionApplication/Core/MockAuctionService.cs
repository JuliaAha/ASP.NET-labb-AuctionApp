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

    public List<Auction> GetWonAuctions(string userName)
    {
        List<Auction> wonAuctions = new List<Auction>();
        foreach (Auction auction in _auctions)
        {
            if (!auction.IsActive() && auction.Bids.First().UserName.Equals(userName))
            {
                wonAuctions.Add(auction);
            }
        }
        return wonAuctions;
    }

    public List<Auction> GetMyActive(string userName)
    {
        List<Auction> myActiveAuctions = new List<Auction>();
        foreach (Auction auction in _auctions)
        {
            if (auction.IsActive())
            {
                foreach (Bid bids in auction.Bids)
                {
                    if (bids.UserName.Equals(userName) && !myActiveAuctions.Contains(auction))
                    {
                        myActiveAuctions.Add(auction);
                    }
                }
            } 
        }
        myActiveAuctions.Sort();
        return myActiveAuctions;
    }

    public Auction GetById(int id)
    {
        return _auctions.Find(a => a.AuctionId == id && a.IsActive());
    }

    public void Add(string title, string userName, string description, DateTime endDate, double startingPrice)
    {
        throw new NotImplementedException();
    }

    public void Add(string userName, string Title)
    {
        throw new NotImplementedException("MockProjectService.Add");
    }
    private static readonly List<Auction> _auctions = new();
    
    //C# style static initializer
    static MockAuctionService()
    {
        Auction a1 = new Auction(1,"Katt","julg@kth.se", "En fin katt", DateTime.Today.AddDays(1).AddHours(5), 300);
        Auction a2 = new Auction(2,"Kattt","emma@kth.se", "En ful katt", DateTime.Today.AddDays(2).AddHours(3), 5000);
        Auction a3 = new Auction(3,"Katttt","julg@kth.se", "En svart katt", DateTime.Today.AddDays(3).AddHours(1), 200);
        Auction a4 = new Auction(4, "Kattttt", "julg@kth.se", "En vit katt", DateTime.Today.AddDays(-1).AddHours(23), 500);
        
        a2.AddBid(new Bid(1, "julg@kth.se", 5100));
        a2.AddBid(new Bid(2, "julg@kth.se", 5150));
        a2.AddBid(new Bid(3, "julg@kth.se", 5170));
        a2.AddBid(new Bid(4, "julg@kth.se", 5599));
        _auctions.Add(a1);
        _auctions.Add(a2);
        _auctions.Add(a3);
        _auctions.Add(a4);
    }
}