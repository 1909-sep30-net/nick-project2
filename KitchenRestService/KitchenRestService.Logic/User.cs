using System.Collections.Generic;

namespace KitchenRestService.Logic
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public bool Admin { get; set; }

        public ISet<FridgeItem> FridgeItems { get; set; } = new HashSet<FridgeItem>();
    }
}
