using System.Data;
using AuctionApplication.Core;
using AuctionApplication.Core.Exceptions;
using AuctionApplication.Core.Interfaces;
using AuctionApplication.Core.Interfaces.Interfaces;
using AuctionApplication.Models.Auctions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuctionApplication.Controllers
{
    [Authorize]
    public class AuctionController : Controller
    {
        private IAuctionService _auctionService;

        public AuctionController(IAuctionService auctionService)
        {
            _auctionService = auctionService;
        }
        // GET: AuctionController
        public ActionResult Index()
        {
            //List<Auction> auctions = _auctionService.GetAllByUserName("dummy");  TODO: Changed to GetAllActive;
            List<Auction> auctions = _auctionService.GetAllActive();
            List<AuctionVm> auctionsVms = new List<AuctionVm>();
            
            foreach (Auction auction in auctions)
            {
                auctionsVms.Add(AuctionVm.FromAuction(auction));
            }
            return View(auctionsVms);
        }
        public ActionResult Pending()
        {
            List<Auction> auctions = _auctionService.GetMyActive(User.Identity.Name);
            List<AuctionVm> auctionsVms = new List<AuctionVm>();
            
            foreach (Auction auction in auctions)
            {
                auctionsVms.Add(AuctionVm.FromAuction(auction));
            }
            return View(auctionsVms);
        }
        
        public ActionResult Won()
        {
            List<Auction> auctions = _auctionService.GetWonAuctions(User.Identity.Name);
            List<AuctionVm> auctionsVms = new List<AuctionVm>();
            
            foreach (Auction auction in auctions)
            {
                auctionsVms.Add(AuctionVm.FromAuction(auction));
            }
            return View(auctionsVms);
        }

        // GET: AuctionController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Auction auction = _auctionService.GetById(id);
                if (auction == null) return BadRequest();
                
                var detailsVm = AuctionDetailsVm.FromAuction(auction);  // Assuming this method exists
                detailsVm.Id = id;  // Set AuctionId explicitly
                //AuctionDetailsVm detailsVm = AuctionDetailsVm.FromAuction(auction); //är kan man använda en mapper
                return View(detailsVm);
            }
            catch (DataException ex)
            {
                return BadRequest();
            }
        }
        
        // POST: AuctionController/Create
        public ActionResult CreateBid(int id)
        {
            ViewBag.AuctionId = id; // Set the auction ID for the view
            return View();
        }

        // POST: AuctionsController/Create/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBid(CreateBidVm createBidVm, int id)
        {
            try
            {
                double amount = createBidVm.Amount;
                string userName = User.Identity.Name; // Assuming user is authenticated
                Console.WriteLine($"Creating bid for auction ID: {id} by user: {userName} with amount: {amount}");
                if (ModelState.IsValid)
                {
                    // Attempt to add the bid
                    ViewBag.AuctionId = id; // Set the auction ID for the view
                    _auctionService.AddBid(id, userName, amount);
                    TempData["Success"] = "Ditt bud har lagts framgångsrikt!";
                }
                return RedirectToAction("Details", new { id = id });
            }
            catch (AuctionOutDatedExceptions ex)
            {
                TempData["ErrorMessage"] = "You cant place a bid on a closed auction."; // Store the error message
                return RedirectToAction("Details", new { id });
            }
            catch (AddBidToOwnAuctionException ex)
            {
                TempData["ErrorMessage"] = "You can not bid on your own auction."; // Store the error message
                return RedirectToAction("Details", new { id });
            }
            catch (ToLowBidException ex)
            {
                TempData["ErrorMessage"] = "Your bid does not exceed current price"; // Store the error message
                return RedirectToAction("Details", new { id });
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] =
                    "An invalid operation occurred. Please try again later."; // General error message
                return RedirectToAction("Details", new { id });
            }
            catch (DataException ex)
            {
                TempData["ErrorMessage"] = "There was a problem with the data. Please try again."; // Data error message
                return RedirectToAction("Details", new { id });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] =
                    "An unexpected error occurred. Please try again later."; // Generic error message
                return RedirectToAction("Details", new { id });
            }
        }
        
        // GET: AuctionController/Create
        public ActionResult CreateAuction()
        {
            return View();
        }
        // POST: AuctionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateAuction(CreateAuctionsVm createAuctionsVm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string title = createAuctionsVm.Title;
                    string description = createAuctionsVm.Description;
                    DateTime endDate = createAuctionsVm.EndDate;
                    double startingPrice = createAuctionsVm.StartingPrice;
                    string auctionOwner = User.Identity.Name;;
                    
                    _auctionService.Add(title, auctionOwner, description, endDate, startingPrice);
                    TempData["SuccessMessage"] = "Auktionen har skapats framgångsrikt!";
                    return RedirectToAction("Index");
                }
                return View(createAuctionsVm);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Ett fel inträffade när auktionen skulle skapas: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // POST: AuctionController/Create
        public ActionResult ChangeDescription(int id)
        {
            ViewBag.AuctionId = id; // Set the auction ID for the view
            return View();
        }
        // POST: AuctionsController/Create/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeDescription(ChangeDescriptionVm changeDescriptionVm, int id)
        {
            try
            {
                string userName = User.Identity.Name;
                ; // Assuming user is authenticated
                if (ModelState.IsValid)
                {
                    ViewBag.AuctionId = id; // Set the auction ID for the view
                    _auctionService.UpdateDescription(id, userName, changeDescriptionVm.Description);
                    return RedirectToAction("Details", new { id = id });
                }

                return View(changeDescriptionVm); // Return view with validation errors if any
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = "Hoppsan, något gick fel.";
                return RedirectToAction("Details", new { id = id });
            }
            catch (DataException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(changeDescriptionVm);
            }
        }
    }
}
