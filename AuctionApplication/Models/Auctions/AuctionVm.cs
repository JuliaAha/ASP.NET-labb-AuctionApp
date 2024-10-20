using System.ComponentModel.DataAnnotations;
using AuctionApplication.Core;
using AuctionApplication.Core.Interfaces;

namespace AuctionApplication.Models.Auctions;

public class AuctionVm
{
    [ScaffoldColumn(false)] 
    public int Id { get; set; }
    
    [DisplayFormat(DataFormatString = "{0:N2}")]
    public double StartingPrice { get; set; }
    public string AuctionOwner { get; set; }
    public string Title { get; set; }
    
    [Display(Name = "Item description")]
    public string Description { get; set; }
    
    [Display(Name = "End date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    
    public static AuctionVm FromAuction(Auction auction)
    {
        return new AuctionVm()
        {
            Id = auction.AuctionId,
            Title = auction.Title,
            Description = auction.Description,
            AuctionOwner = auction.AuctionOwner,
            StartingPrice = auction.StartingPrice,
            EndDate = auction.EndDate,
            IsActive = auction.IsActive()
        };
    }
}