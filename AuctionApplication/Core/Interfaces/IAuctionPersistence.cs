namespace AuctionApplication.Core.Interfaces;

public interface IAuctionPersistence
{
    List<Auction> GetAllActive();
    List<Auction> GetWonAuctions(string userName);
    List<Auction> GetMyActive(string userName);

    Auction GetById(int id);
    void Save(Auction auctions);
}