using System.ComponentModel.DataAnnotations;

namespace AuctionApplication.Models.Auctions;

public class ChangeDescriptionVm
{
    [Required]
    [StringLength(500, ErrorMessage = "Max length 500 characters")]
    public string? Description { get; set; }
}