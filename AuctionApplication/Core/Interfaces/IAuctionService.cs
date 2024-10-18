namespace AuctionApplication.Core.Interfaces.Interfaces;

public interface IAuctionService
{
    List<Auction> getAllActive();
    
    Auction GetById(int id);
    
    void Add(string userName, string title);
}