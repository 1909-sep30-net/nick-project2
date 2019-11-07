using System;
using System.Collections.Generic;
using System.Text;

namespace KitchenRestService.Logic
{
    public class FridgeItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Expiration { get; set; }
        public int OwnerId { get; set; }

        public User Owner { get; set; }
    }
}
