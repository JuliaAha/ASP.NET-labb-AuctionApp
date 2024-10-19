namespace AuctionApplication.Core.Interfaces;

public class Auction : IComparable<Auction>
{
    public int AuctionId { get; set; }
    public string AuctionTitle { get; set; }
    public DateTime AuctionEndDate { get; set; }
    public string AuctionDescription { get; set; }
    public string AuctionOwner { get; set; }
    
    public string UserName { get; set; }
    
    private List<Bids> _bids = new List<Bids>();
    public IEnumerable<Bids> Bids => _bids;

    public Auction(string title, string userName)
    {
        AuctionTitle = title;
        UserName = userName;
        AuctionEndDate = AuctionEndDate.AddDays(5);
    }

    public Auction() { }

    public Auction(int auctionId, string title, string userName, string auctionDescription, DateTime auctionEndDate)
    {
        AuctionId = auctionId;
        AuctionTitle = title;
        UserName = userName;
        AuctionEndDate = auctionEndDate;
        AuctionDescription = auctionDescription;
    }
    
    public void AddBid(Bids newBid)
    {
        _bids.Add(newBid);
    }

    public bool IsActive()
    {
        return AuctionEndDate > DateTime.Now;
    }

    public int CompareTo(Auction other)
    {
        return this.AuctionEndDate.CompareTo(other.AuctionEndDate);
    }
    
    public override string ToString()
    {
        return $"{AuctionId}: {AuctionTitle} - completed: {IsActive()}";
    }
}