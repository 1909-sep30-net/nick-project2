using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KitchenRestService.Logic
{
    public interface IKitchenRepo
    {
        Task<IEnumerable<FridgeItem>> GetAllFridgeItemsAsync();
        Task<IEnumerable<FridgeItem>> GetExpiredFridgeItemsAsync(DateTime cutoff);
        Task<IEnumerable<FridgeItem>> GetOwnedFridgeItemsAsync(int userId);
        Task<FridgeItem> GetFridgeItemAsync(int id);
        Task<FridgeItem> CreateFridgeItemAsync(FridgeItem item);
        Task<bool> UpdateFridgeItemAsync(FridgeItem item);
        Task<bool> DeleteFridgeItemAsync(int id);
    }
}
