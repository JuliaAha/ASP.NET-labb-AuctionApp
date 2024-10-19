using AuctionApplication.Core;
using AuctionApplication.Core.Interfaces;
using AuctionApplication.Persistence;
using AutoMapper;

namespace AuctionApplication.Mappers;

public class BidProfile : Profile
{
    public BidProfile()
    {
        CreateMap<BidDb, Bid>().ReverseMap();
    }
}