using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenRestService.Api.Models
{
    public class Fridge
    {
        public ISet<FridgeItem> Items { get; } = new HashSet<FridgeItem>
        {
            new FridgeItem { Id = 1, Name = "coffee", Expiration = new DateTime(2019, 12, 12) },
            new FridgeItem { Id = 2, Name = "mushrooms", Expiration = new DateTime(2019, 12, 12) },
            new FridgeItem { Id = 3, Name = "mold", Expiration = new DateTime(2018, 12, 12) }, // expired
            new FridgeItem { Id = 4, Name = "milk", Expiration = new DateTime(2019, 10, 1) } // expired
        };
    }
}
