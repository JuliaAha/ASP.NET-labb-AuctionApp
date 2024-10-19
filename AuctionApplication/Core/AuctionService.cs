using AuctionApplication.Core.Interfaces;
using AuctionApplication.Core.Interfaces.Interfaces;

namespace AuctionApplication.Core;

public class AuctionService(IAuctionPersistence auctionPersistence) : IAuctionService
{
    public List<Auction> GetAllActive()
    {
        List<Auction> auctions = auctionPersistence.GetAllActive();
        return auctions;
    }

    public List<Auction> GetWonAuctions(string userName)
    {
        List<Auction> auctions = auctionPersistence.GetWonAuctions(userName);
        return auctions;
    }

    public List<Auction> GetMyActive(string userName)
    {
        List<Auction> auctions = auctionPersistence.GetMyActive(userName);
        return auctions;
    }

    public Auction GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Add(string userName, string title)
    {
        throw new NotImplementedException();
    }
}