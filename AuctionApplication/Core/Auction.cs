namespace AuctionApplication.Core.Interfaces;

public class Auction
{
    public int AuctionId { get; set; }
    public string AuctionTitle { get; set; }
    public DateTime AuctionEndDate { get; set; }
    public string AuctionDescription { get; set; }
    public string AuctionOwner { get; set; }
    
    public string UserName { get; set; }
    
    private List<Bids> _bids = new List<Bids>();
    public IEnumerable<Bids> Bids => _bids;

    public Auction(string title, string userName, string auctionDescription)
    {
        AuctionTitle = title;
        UserName = userName;
        AuctionEndDate = AuctionEndDate.AddDays(5);
        AuctionDescription = auctionDescription;
    }

    public Auction() { }

    public Auction(int auctionId, string title, string userName, string auctionDescription)
    {
        AuctionId = AuctionId;
        AuctionTitle = title;
        UserName = userName;
        AuctionEndDate = AuctionEndDate.AddDays(5);
        AuctionDescription = auctionDescription;
    }
    
    public void AddBid(Bids newBid)
    {
        _bids.Add(newBid);
    }

    public bool IsActive()
    {
        if (_bids.Count == 0) return true;
        return _bids.All(b => AuctionEndDate > DateTime.Now);
    }
    public override string ToString()
    {
        return $"{AuctionId}: {AuctionTitle} - completed: {IsActive()}";
    }

    
}