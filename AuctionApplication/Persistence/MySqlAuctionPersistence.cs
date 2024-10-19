using System.Data;
using AuctionApplication.Core.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AuctionApplication.Persistence;

public class MySqlAuctionPersistence : IAuctionPersistence {
    
    private readonly AuctionDbContext _dbContext;
    private readonly IMapper _mapper;

    public MySqlAuctionPersistence(AuctionDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public List<Auction> GetAllActive()
    {
        var auctionDbs = _dbContext.AuctionDbs
            .Where(a => a.EndDate > DateTime.Now)
            .ToList();

        List<Auction> result = new List<Auction>();
        foreach (AuctionDb adb in auctionDbs)
        {
            Auction auction = _mapper.Map<Auction>(adb);
            result.Add(auction);
        }
        result.Sort();
        return result;
    }

    public List<Auction> GetWonAuctions(string userName)
    {
        var auctionDbs = _dbContext.AuctionDbs
            .Where(a => a.EndDate <= DateTime.Now)
            .ToList();

        List<Auction> result = new List<Auction>();
        foreach (AuctionDb adb in auctionDbs)
        {
            Auction auction = _mapper.Map<Auction>(adb);
            if (auction.Bids.First().UserName.Equals(userName))
            {
                result.Add(auction);
            }
        }
        result.Sort();
        return result;
    }

    public List<Auction> GetMyActive(string userName)
    {
        var auctionDbs = _dbContext.AuctionDbs
            .Where(a => a.EndDate > DateTime.Now)
            .ToList();

        List<Auction> result = new List<Auction>();
        foreach (AuctionDb adb in auctionDbs)
        {
            Auction auction = _mapper.Map<Auction>(adb);

            if (auction.Bids.Any(bid => bid.UserName == userName))
            {
                result.Add(auction);
            }
        }
        result.Sort();
        return result;
    }

    public Auction GetById(int id)
    {
        AuctionDb auctionDb = _dbContext.AuctionDbs
            .Where(p => p.Id == id)
            .Include(p => p.BidDbs)
            .FirstOrDefault(); //null if not found
        
        if (auctionDb == null) throw new DataException("Auction not found");
        
        Auction auction = _mapper.Map<Auction>(auctionDb);
        foreach (BidDb bidDb in auctionDb.BidDbs)
        {
            Bid bid = _mapper.Map<Bid>(bidDb);
            auction.AddBid(bid);
        }
        return auction;
    }

    public void Save(Auction auctions)
    {
        AuctionDb pdb = _mapper.Map<AuctionDb>(auctions);
        _dbContext.AuctionDbs.Add(pdb);
        _dbContext.SaveChanges();
    }
}