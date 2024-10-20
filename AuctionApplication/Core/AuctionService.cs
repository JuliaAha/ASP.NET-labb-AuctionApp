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

    public void UpdateDescription(int auctionId, string userName, string description)
    {
        Console.WriteLine($"Attempting to add bid for auction ID: {auctionId}, by user: {userName}");
        if (string.IsNullOrWhiteSpace(userName)) 
            throw new DataException("Username is required");
        
        // Retrieve the auction by ID
        Auction auction = _auctionPersistence.GetById(auctionId);
        if (auction == null) 
            throw new DataException("Auction not found");

        if (auction.AuctionOwner.Equals(userName))
        {
            // Set description
            auction.UpdateDescription(description);
            // Save the auction with the new bid
            _auctionPersistence.Save(auction);
        }
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

    public void AddBid(int auctionId, string userName, double amount)
    {
        Console.WriteLine($"Attempting to add bid for auction ID: {auctionId}, amount: {amount}, by user: {userName}");
        if (string.IsNullOrWhiteSpace(userName)) 
            throw new DataException("Username is required");

        // Retrieve the auction by ID
        Auction auction = _auctionPersistence.GetById(auctionId);
        if (auction == null) 
            throw new DataException("Auction not found");

        // Log the current highest bid for debugging
        if (auction.Bids.Any())
        {
            double currentHighestBid = auction.Bids.Max(b => b.Amount);
            Console.WriteLine($"Current highest bid: {currentHighestBid}");
            if (amount <= currentHighestBid)
                throw new InvalidOperationException("The bid amount must be greater than the current highest bid.");
        }

        // Create and add the new bid
        Bid bid = new Bid(userName, amount);
        auction.AddBid(bid);

        // Save the auction with the new bid
        _auctionPersistence.Save(auction);
    }
}