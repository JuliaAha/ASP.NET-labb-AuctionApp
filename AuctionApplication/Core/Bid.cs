namespace AuctionApplication.Core.Interfaces;

public class Bid : IComparable<Bid>
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public double Amount { get; set; }

    private DateTime _bidLayed;
    public DateTime BidLayed  { get => _bidLayed; }
    
    
    public Bid(int id, string userName, double amount)
    {
        Id = id;
        UserName = userName;
        _bidLayed = DateTime.Now; 
        Amount = amount;
    }
    public int CompareTo(Bid other)
    {
        return -this.Amount.CompareTo(other.Amount);
    }
    
    public override string ToString()
    {
        return $"{Id}: {UserName}: {Amount} {BidLayed}";
    }
}