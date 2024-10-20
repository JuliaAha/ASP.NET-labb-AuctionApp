namespace AuctionApplication.Core.Interfaces.Interfaces;

public interface IAuctionService
{
    List<Auction> GetAllActive();
    List<Auction> GetWonAuctions(string userName);
    List<Auction> GetMyActive(string userName);
    
    
    Auction GetById(int id);
    
    void Add(string title, string auctionOwner, string description, DateTime endDate, double startingPrice);
}