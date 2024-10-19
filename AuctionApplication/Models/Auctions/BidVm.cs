using System.ComponentModel.DataAnnotations;
using AuctionApplication.Core.Interfaces;

namespace AuctionApplication.Models.Auctions;

public class BidVm
{
    [ScaffoldColumn(false)]    
    public int Id { get; set; }                                    
    public string UserName { get; set; }                           
    public double Amount { get; set; }  
    [Display(Name = "Lagt bud")]
    [DisplayFormat(DataFormatString = "{0:N2}")]
                                                               
    private DateTime _bidLayed;    
    [Display(Name = "Lagt")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}")]
    public DateTime BidLayed { get; set; }                
                                                               
                                                               
    public static BidVm FromBids(Bid bid)
    {
        return new BidVm()
        {
            Id = bid.Id,
            UserName = bid.UserName,
            Amount = bid.Amount,
            BidLayed = bid.BidLayed
        };
    }                                                           
}