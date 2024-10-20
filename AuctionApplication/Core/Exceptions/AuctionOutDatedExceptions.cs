namespace AuctionApplication.Core.Exceptions;

public class AuctionOutDatedExceptions : InvalidOperationException
{
     public AuctionOutDatedExceptions() 
            : base("Auktionen har gått ut.") 
        {
        }
    
        public AuctionOutDatedExceptions(string message) 
            : base(message) 
        {
        }
    
        public AuctionOutDatedExceptions(string message, Exception inner) 
            : base(message, inner) 
        {
        }
}