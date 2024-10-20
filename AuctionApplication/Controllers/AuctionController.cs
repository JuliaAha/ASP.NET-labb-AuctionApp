using System.Data;
using AuctionApplication.Core;
using AuctionApplication.Core.Interfaces;
using AuctionApplication.Core.Interfaces.Interfaces;
using AuctionApplication.Models.Auctions;
using Microsoft.AspNetCore.Mvc;

namespace AuctionApplication.Controllers
{
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
            List<Auction> auctions = _auctionService.GetMyActive("julg@kth.se");
            List<AuctionVm> auctionsVms = new List<AuctionVm>();
            
            foreach (Auction auction in auctions)
            {
                auctionsVms.Add(AuctionVm.FromAuction(auction));
            }
            return View(auctionsVms);
        }
        
        public ActionResult Won()
        {
            List<Auction> auctions = _auctionService.GetWonAuctions("julg@kth.se");
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
                
                // var createBidVm = new CreateBidVm
                // {
                //     AuctionId = id  // Pass the auction ID to the bid form
                // };
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
                  string userName = "lovat@kth.se"; // Assuming user is authenticated
                  Console.WriteLine($"Creating bid for auction ID: {id} by user: {userName} with amount: {amount}");
                  if (ModelState.IsValid)
                  {
                    // Attempt to add the bid
                    ViewBag.AuctionId = id; // Set the auction ID for the view
                    _auctionService.AddBid(id, userName, amount);
                    return RedirectToAction("Details", new { id = id });
                  }
                  return View(createBidVm); // Return view with validation errors if any
            }
            catch (DataException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(createBidVm);
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
                    string auctionOwner = "julg@kth.se";
                    
                    _auctionService.Add(title, auctionOwner, description, endDate, startingPrice);
                    return RedirectToAction("Index");
                }
                return View(createAuctionsVm);
            }
            catch
            {
                return View(createAuctionsVm);
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
                string userName = "julg@kth.se"; // Assuming user is authenticated
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
        // GET: AuctionController/Edit/5
       /* public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AuctionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AuctionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AuctionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }*/
    }
}
