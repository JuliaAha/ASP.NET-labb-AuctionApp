using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuctionApplication.Persistence;

public class BidDb
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(128)]
    public string UserName { get; set; }

    [Required]
    public double Amount { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime BidDate { get; set; }

    [ForeignKey("AuctionId")]
    public AuctionDb AuctionDb { get; set; }

    public int AuctionId { get; set; }
}