using AuctionApplication.Core;

namespace AuctionApplication.Core;

public class Auction : IComparable<Auction>
{
    public int AuctionId { get; set; }
    public double StartingPrice { get; set; }
    public string AuctionTitle { get; set; }
    public DateTime EndDate { get; set; }
    public string AuctionDescription { get; set; }
    public string AuctionOwner { get; set; }
   
    
    private List<Bid> _bids = new List<Bid>();
    public IEnumerable<Bid> Bids => _bids;

    public Auction(string title, string auctionOwner)
    {
        AuctionTitle = title;
        AuctionOwner = auctionOwner;
        EndDate = EndDate.AddDays(5);
    }

    public Auction() { }

    public Auction(int auctionId, string title, string auctionOwner, string auctionDescription, DateTime endDate, double startingPrice)
    {
        AuctionId = auctionId;
        AuctionTitle = title;
        AuctionOwner = auctionOwner;
        EndDate = endDate;
        AuctionDescription = auctionDescription;
        StartingPrice = startingPrice;
    }
    
    public void AddBid(Bid newBid)
    {
        if (AuctionOwner.Equals(newBid.UserName))
        {
            throw new NotImplementedException();
        }
        if (EndDate.CompareTo(DateTime.Now) <= 0)
        {
            throw new NotImplementedException();
        }
        if (_bids.Count == 0 && newBid.Amount <= StartingPrice)
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
        return EndDate > DateTime.Now;
    }

    public int CompareTo(Auction other)
    {
        return EndDate.CompareTo(other.EndDate);
    }
    
    public override string ToString()
    {
        return $"{AuctionId}: {AuctionTitle} - completed: {IsActive()}";
    }
}