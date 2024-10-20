using Microsoft.Build.Framework;

namespace AuctionApplication.Models.Auctions;

public class CreateBidVm
{
    [Required]
    public double Amount { get; set; }
}