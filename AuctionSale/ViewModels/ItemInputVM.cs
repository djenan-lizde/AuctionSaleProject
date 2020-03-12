using Microsoft.AspNetCore.Http;
using System;

namespace AuctionSale.ViewModels
{
    public class ItemInputVM
    {
        public string ProductName { get; set; }
        public double Price { get; set; }
        public DateTime EndTime { get; set; }
        public IFormFile PicturePath { get; set; }
        public string Picture { get; set; }
    }
}
