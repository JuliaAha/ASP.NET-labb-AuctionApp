using System.ComponentModel.DataAnnotations;

namespace AuctionApplication.Models.Auctions;

public class CreateAuctionsVm
{
    [Required]
    [StringLength(128, ErrorMessage = "Max length 128 characters")]

    public string? Title { get; set; }
}