using System.ComponentModel.DataAnnotations;
using AuctionApplication.Core;
using AuctionApplication.Core.Interfaces;

namespace AuctionApplication.Models.Auctions;

public class AuctionDetailsVm
{
    [ScaffoldColumn(false)] 
    public int Id { get; set; }
    public double StartingPrice { get; set; }
    public string Title { get; set; }
    
    public string Description { get; set; }
    
    [Display(Name = "Auction ends at")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    
    [Display(Name = "Auction owner")]
    public string AuctionOwner { get; set; }
    public List<BidVm> BidVMs { get; set; } = new();

    public static AuctionDetailsVm FromAuction(Auction auction)
    {
        var detailsVM = new AuctionDetailsVm()
        {
            Id = auction.AuctionId,
            Title = auction.AuctionTitle,
            Description = auction.AuctionDescription,
            EndDate = auction.EndDate,
            IsActive = auction.IsActive(),
            AuctionOwner = auction.AuctionOwner,
            StartingPrice = auction.StartingPrice
        };
        foreach (var bid in auction.Bids)
        {
           detailsVM.BidVMs.Add(BidVm.FromBids(bid)); 
        }
        return detailsVM;
    }
}