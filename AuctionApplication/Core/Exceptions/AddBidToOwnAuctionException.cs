namespace AuctionApplication.Core.Exceptions;

public class AddBidToOwnAuctionException : InvalidOperationException
{
    public AddBidToOwnAuctionException() 
        : base("Du får inte buda på din egen auktion.") 
    {
    }

    public AddBidToOwnAuctionException(string message) 
        : base(message) 
    {
    }

    public AddBidToOwnAuctionException(string message, Exception inner) 
        : base(message, inner) 
    {
    }
}