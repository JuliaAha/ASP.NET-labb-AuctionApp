using AuctionApplication.Core.Interfaces;

namespace AuctionApplication.Core;

public class MockAuctionService
{
    public List<Auction> GetAllByUserName(string userName)
    {
        return _auctions;
    }
    
    public Auction GetById(int id, string userName)
    {
        return _auctions.Find(a => a.AuctionId == id && a.UserName == userName); 
       
    }

    public void Add(string userName, string Title)
    {
        throw new NotImplementedException("MockProjectService.Add");
    }
    private static readonly List<Auction> _auctions = new();
    
    //C# style static initializer
    static MockAuctionService()
    {
        Auction a1 = new Auction(1,"Byrå","julg@kth.se","Fin brun byrå");
        Auction a2 = new Auction(1,"Tavla","julg@kth.se","Fin brun tavla");
        a2.AddBid(new Bids(1, "julg@kth.se",50));
        a2.AddBid(new Bids(1, "julg@kth.se",100));
        _auctions.Add(a1);
        _auctions.Add(a2);
    }
}