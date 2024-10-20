namespace AuctionApplication.Core.Exceptions;

public class ToLowBidException : InvalidOperationException
{
    public ToLowBidException() 
        : base("Ditt bud måste vara högre än det nuvarande budet.") 
    {
    }

    public ToLowBidException(string message) 
        : base(message) 
    {
    }

    public ToLowBidException(string message, Exception inner) 
        : base(message, inner) 
    {
    }
}