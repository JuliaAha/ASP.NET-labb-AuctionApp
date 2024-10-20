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

    public void Add(string title, string auctionOwner, string description, DateTime endDate, double startingPrice)
    {
        if (title == null || title.Length > 128) throw new DataException("Auction title must be between 1 and 128 characters");
        if (description == null || description.Length > 500) throw new DataException("Auction description must be between 1 and 500 characters");
        if (endDate < DateTime.Now) throw new DataException("Auction end date must be after now");
        if (auctionOwner == null) throw new DataException("Auction owner is required");
        if (startingPrice < 0)throw new DataException("Auction start price must be >= 0");
        
        Auction auction = new Auction(title, auctionOwner, description, endDate, startingPrice);
        _auctionPersistence.Save(auction);
    }
}