namespace AuctionApplication.Core.Interfaces;

public class Bids
{
    public int Id { get; set; }
    public string userName { get; set; }
    public double Amount { get; set; }

    private DateTime _bidLayed;
    public DateTime BidLayed  { get => _bidLayed; }
    

    public Bids(int id, string userName, double amount)
    {
        Id = id;
        userName = userName;
        _bidLayed = DateTime.Now;
        amount = Math.Round(amount, 2);
    }
}