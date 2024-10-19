using System.ComponentModel.DataAnnotations;
using AuctionApplication.Core.Interfaces;

namespace AuctionApplication.Models.Auctions;

public class AuctionDetailsVm
{
    [ScaffoldColumn(false)] 
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    [Display(Name = "Auction End date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
    public DateTime AuctionEndDate { get; set; }
    public bool IsActive { get; set; }

    public List<BidVm> BidVMs { get; set; } = new();

    public static AuctionDetailsVm FromAuction(Auction auction)
    {
        var detailsVM = new AuctionDetailsVm()
        {
            Id = auction.AuctionId,
            Title = auction.AuctionTitle,
            Description = auction.AuctionDescription,
            AuctionEndDate = auction.AuctionEndDate,
            IsActive = auction.IsActive()
        };
        foreach (var bid in auction.Bids)
        {
           detailsVM.BidVMs.Add(BidVm.FromBids(bid)); 
        }
        return detailsVM;
    }
}