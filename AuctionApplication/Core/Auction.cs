namespace AuctionApplication.Core.Interfaces;

public class Auction : IComparable<Auction>
{
    public int AuctionId { get; set; }
    public double StartingPrice { get; set; }
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

    public Auction(int auctionId, string title, string userName, string auctionDescription, DateTime auctionEndDate, double startingPrice)
    {
        AuctionId = auctionId;
        AuctionTitle = title;
        UserName = userName;
        AuctionEndDate = auctionEndDate;
        AuctionDescription = auctionDescription;
        StartingPrice = startingPrice;
    }
    
    public void AddBid(Bids newBid)
    {
        if (AuctionEndDate.CompareTo(DateTime.Now) <= 0)
        {
            throw new NotImplementedException();
        }
        if (_bids.Count == 0 && newBid.Amount < StartingPrice)
        {
            throw new NotImplementedException();
        }
        if (_bids.Count > 0)
        {
            if (_bids.First().CompareTo(newBid) <= 0)
            {
                throw new ArgumentException("Bid must be higher than the current highest bid");
            }
        }
        _bids.Add(newBid);
        _bids.Sort(); 
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