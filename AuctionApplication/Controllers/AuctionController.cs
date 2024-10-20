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
            // List<AuctionVm> auctionVms = auctions.Select(AuctionVm.FromAuction).ToList();
            //
            // foreach (var auctionVm in auctionVms)
            // {
            //     Console.WriteLine($"Auction: {auctionVm.Title}, Description: {auctionVm.Description}");
            // }
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

                AuctionDetailsVm detailsVm = AuctionDetailsVm.FromAuction(auction); //är kan man använda en mapper
                return View(detailsVm);
            }
            catch (DataException ex)
            {
                return BadRequest();
            }
        }
        /*
        // GET: AuctionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AuctionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: AuctionController/Edit/5
        public ActionResult Edit(int id)
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
