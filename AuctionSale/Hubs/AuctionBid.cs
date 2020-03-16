using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionSale.Hubs
{
    public class AuctionBid : Hub
    {
        public async Task Send(string message, int newPrice, int id)
        {
            await Clients.All.SendAsync("BidUpdate", message, newPrice, id);
        }

        public async Task NewItem(string message)
        {
            await Clients.All.SendAsync("NewItemMessage", message);
        }
    }
}
