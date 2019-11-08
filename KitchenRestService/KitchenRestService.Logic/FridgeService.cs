using System;
using System.Linq;
using System.Threading.Tasks;

namespace KitchenRestService.Logic
{
    public class FridgeService : IFridgeService
    {
        private readonly IKitchenRepo _kitchenRepo;

        public FridgeService(IKitchenRepo kitchenRepo)
        {
            _kitchenRepo = kitchenRepo ?? throw new ArgumentNullException(nameof(kitchenRepo));
        }

        public async Task<bool> CleanFridgeAsync()
        {
            var expired = await _kitchenRepo.GetExpiredFridgeItemsAsync(DateTime.Now);
            if (!expired.Any())
            {
                return false;
            }
            foreach (var item in expired)
            {
                await _kitchenRepo.DeleteFridgeItemAsync(item.Id);
            }
            return true;
        }
    }
}
