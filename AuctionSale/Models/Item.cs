using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionSale.Models
{
    public class Item : IEntity
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int UnitsInStock { get; set; }
        public string Picture { get; set; }
        public bool IsDeleted { get; set; }
        public string ProductNumber { get; set; }
    }
}
