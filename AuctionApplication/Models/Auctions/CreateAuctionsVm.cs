using System.ComponentModel.DataAnnotations;

namespace AuctionApplication.Models.Auctions;

public class CreateAuctionsVm
{
    [Required]
    [StringLength(128, ErrorMessage = "Max length 128 characters")]
    public string? Title { get; set; }
    
    [Required]
    [StringLength(500, ErrorMessage = "Max length 500 characters")]
    public string? Description { get; set; }
    
    [Required]
    public double? StartingPrice { get; set; }
    
    [Required]
    public DateTime? EndDate { get; set; }
}