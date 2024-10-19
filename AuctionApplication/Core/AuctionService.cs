using System.Data;
using AuctionApplication.Core;
using AuctionApplication.Core.Interfaces;
using AuctionApplication.Core.Interfaces.Interfaces;

namespace AuctionApp.Core;

public class AuctionService : IAuctionService
{
    private readonly IAuctionPersistence _auctionPersistence;

    public AuctionService(IAuctionPersistence auctionPersistence)
    {
        _auctionPersistence = auctionPersistence;
    }

    public List<Auction> GetAllActive()
    {
        List<Auction> auctions = _auctionPersistence.GetAllActive();
        return auctions;
    }

    public List<Auction> GetWonAuctions(string userName)
    {
        List<Auction> auctions = _auctionPersistence.GetWonAuctions(userName);
        return auctions;
    }

    public List<Auction> GetMyActive(string userName)
    {
        List<Auction> auctions = _auctionPersistence.GetMyActive(userName);
        return auctions;
    }

    public Auction GetById(int id)
    {
        Auction auction = _auctionPersistence.GetById(id);
        if (auction == null) throw new DataException("Auction not found");
        return auction;
    }

    public void Add(string userName, string title)
    {
        throw new NotImplementedException();
    }
}