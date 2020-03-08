using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionSale.Models;
using AuctionSale.Services;
using AuctionSale.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AuctionSale.Controllers
{
    public class ItemController : Controller
    {
        private readonly IData<Item> _dataItem;
        private readonly IMapper _mapper;
        private readonly IImagesService _imagesService;

        public ItemController(IData<Item> dataItem,IMapper mapper,
            IImagesService imagesService)
        {
            _dataItem = dataItem;
            _mapper = mapper;
            _imagesService = imagesService;
        }
        public IActionResult Index()
        {
            return View(_dataItem.Get());
        }
        public IActionResult Create()
        {
            return View(new ItemInputVM());
        }
        public IActionResult Save(ItemInputVM model)
        {
            string uniqueFileName = null;
            if (model.PicturePath != null)
                uniqueFileName = _imagesService.Upload(model.PicturePath, model.PicturePath.FileName);

            var mappedForDB = _mapper.Map<Item>(model);
            mappedForDB.ProductNumber = new Guid().ToString();
            mappedForDB.IsDeleted = false;
            mappedForDB.Picture=uniqueFileName ?? "N/A";

            _dataItem.Add(mappedForDB);
            return RedirectToAction(nameof(Index));
        }
    }
}