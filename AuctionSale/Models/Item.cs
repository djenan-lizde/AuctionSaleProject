using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionSale.Models
{
    public class Item : IEntity
    {
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int UnitsInStock { get; set; }
        [Required]
        public string Picture { get; set; }
        [Required]
        public string ProductNumber { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsFinished { get; set; }
    }
}
