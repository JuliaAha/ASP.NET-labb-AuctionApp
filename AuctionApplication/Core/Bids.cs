namespace AuctionApplication.Core.Interfaces;

public class Bids : IComparable<Bids>
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public double Amount { get; set; }

    private DateTime _bidLayed;
    public DateTime BidLayed  { get => _bidLayed; }
    
    
    public Bids(int id, string userName, double amount)
    {
        Id = id;
        UserName = userName;
        _bidLayed = DateTime.Now; 
        Amount = amount;
    }
    public int CompareTo(Bids other)
    {
        return -this.Amount.CompareTo(other.Amount);
    }
    
    public override string ToString()
    {
        return $"{Id}: {UserName}: {Amount} {BidLayed}";
    }
}