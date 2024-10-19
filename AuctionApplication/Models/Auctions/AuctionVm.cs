using System.ComponentModel.DataAnnotations;
using AuctionApplication.Core.Interfaces;

namespace AuctionApplication.Models.Auctions;

public class AuctionVm
{
    [ScaffoldColumn(false)] 
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    [Display(Name = "Auction End date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
    public DateTime AuctionEndDate { get; set; }
    public bool IsActive { get; set; }
    
    public static AuctionVm FromAuction(Auction auction)
    {
        return new AuctionVm()
        {
            Id = auction.AuctionId,
            Title = auction.AuctionTitle,
            AuctionEndDate = auction.AuctionEndDate,
            IsActive = auction.IsActive(),
            Description = auction.AuctionDescription
        };
    }
}