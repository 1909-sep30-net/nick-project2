using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenRestService.Api.Models
{
    public class Fridge
    {
        public ISet<ApiFridgeItem> Items { get; } = new HashSet<ApiFridgeItem>
        {
            new ApiFridgeItem { Id = 1, Name = "coffee", Expiration = new DateTime(2019, 12, 12) },
            new ApiFridgeItem { Id = 2, Name = "mushrooms", Expiration = new DateTime(2019, 12, 12) },
            new ApiFridgeItem { Id = 3, Name = "mold", Expiration = new DateTime(2018, 12, 12) }, // expired
            new ApiFridgeItem { Id = 4, Name = "milk", Expiration = new DateTime(2019, 10, 1) } // expired
        };
    }
}
