using Microsoft.AspNetCore.Http;

namespace AuctionSale.ViewModels
{
    public class ItemInputVM
    {
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int UnitsInStock { get; set; }
        public IFormFile PicturePath { get; set; }
        public string Picture { get; set; }
    }
}
