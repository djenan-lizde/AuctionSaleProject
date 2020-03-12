using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AuctionSale.Models;
using AuctionSale.Services;
using AuctionSale.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace AuctionSale.Controllers
{
    [Authorize(Roles = "Admin,Member")]
    public class ItemController : Controller
    {
        private readonly IData<Item> _dataItem;
        private readonly IData<BidsItem> _dataBidsItem;
        private readonly IMapper _mapper;
        private readonly IImagesService _imagesService;
        private readonly UserManager<IdentityUser> _userManager;

        public ItemController(IData<Item> dataItem, IMapper mapper,
            IImagesService imagesService, IData<BidsItem> dataBidsItem,
            UserManager<IdentityUser> userManager)
        {
            _dataItem = dataItem;
            _mapper = mapper;
            _imagesService = imagesService;
            _dataBidsItem = dataBidsItem;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var list = _dataItem.Get();
            var listForView = new List<Item>();
            foreach (var item in list.Where(x => x.IsDeleted == false && x.IsFinished == false))
                listForView.Add(item);

            return View(listForView);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(new ItemInputVM());
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Save(ItemInputVM model)
        {
            string uniqueFileName = null;
            if (model.PicturePath != null)
                uniqueFileName = _imagesService.Upload(model.PicturePath, model.PicturePath.FileName);

            var mappedForDB = _mapper.Map<Item>(model);
            Random rnd = new Random();
            mappedForDB.ProductNumber = GenerateProductNumber(10, rnd);

            mappedForDB.IsDeleted = false;
            mappedForDB.Picture = uniqueFileName ?? "N/A";

            _dataItem.Add(mappedForDB);
            return RedirectToAction(nameof(Index));
        }
        public static string GenerateProductNumber(int length, Random random)
        {
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteItem(int id)
        {
            var model = _dataItem.Get(id);
            if (model == null) throw new ArgumentNullException();

            model.IsDeleted = true;

            _dataItem.Update(model);

            return RedirectToAction(nameof(Index));
        }
        public IActionResult UserBid(int id, double newPrice)
        {
            var userBid = new BidsItem
            {
                IsDeleted = false,
                IsWinner = false,
                ItemId = id,
                PriceBidded = newPrice,
                UserId = _userManager.GetUserId(HttpContext.User)
            };
            _dataBidsItem.Add(userBid);
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeclareWinner(int id)
        {
            var allBidItems = _dataBidsItem.Get();
            var product = _dataItem.Get(id);

            var lastItem = allBidItems.LastOrDefault(x => x.ItemId == id);

            product.IsFinished = true;
            _dataItem.Update(product);
            lastItem.IsWinner = true;
            _dataBidsItem.Update(lastItem);
            return RedirectToAction(nameof(Index));
        }
    }
}