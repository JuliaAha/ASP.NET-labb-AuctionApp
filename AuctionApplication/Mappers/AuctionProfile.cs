using AuctionApplication.Core.Interfaces;
using AuctionApplication.Persistence;
using AutoMapper;

namespace AuctionApplication.Mappers;

public class AuctionProfile : Profile
{
    public AuctionProfile()
    {
        CreateMap<AuctionDb, Auction>().ReverseMap();
    }
}