using AuctionSale.Models;
using AuctionSale.ViewModels;
using AutoMapper;

namespace AuctionSale.Services
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<ItemInputVM, Item>();
        }
    }
}
