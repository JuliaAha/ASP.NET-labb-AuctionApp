using System.Data;
using AuctionApplication.Core;
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
                string userName = User.Identity.Name;
                var auction = _auctionService.GetById(id);
                
                if (auction == null)
                {
                    TempData["ErrorMessage"] = "Auktionen hittades inte.";
                    return RedirectToAction("Details", new { id });
                }

                if (auction.AuctionOwner == userName)
                {
                    TempData["ErrorMessage"] = "Du kan inte lägga ett bud på din egen auktion.";
                    return RedirectToAction("Details", new { id });
                }

                if (auction.Bids.Any() && amount <= auction.Bids.Max(b => b.Amount))
                {
                    TempData["ErrorMessage"] = "Budet måste vara högre än nuvarande högsta bud.";
                    return RedirectToAction("Details", new { id });
                }

                ViewBag.AuctionId = id;
                _auctionService.AddBid(id, userName, amount);
                TempData["SuccessMessage"] = "Ditt bud lades framgångsrikt!";
                return RedirectToAction("Details", new { id });
            }
            catch (DataException ex)
            {
                TempData["ErrorMessage"] = "Ett fel inträffade: " + ex.Message;
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
                string userName = User.Identity.Name;; // Assuming user is authenticated
                if (ModelState.IsValid)
                {
                    ViewBag.AuctionId = id; // Set the auction ID for the view
                    _auctionService.UpdateDescription(id, userName, changeDescriptionVm.Description);
                    return RedirectToAction("Details", new { id = id });
                }
                return View(changeDescriptionVm); // Return view with validation errors if any
            }
            catch (DataException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(changeDescriptionVm);
            }
        }
    }
}
