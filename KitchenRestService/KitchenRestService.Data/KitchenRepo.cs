using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KitchenRestService.Logic;
using Microsoft.EntityFrameworkCore;

namespace KitchenRestService.Data
{
    public class KitchenRepo : IKitchenRepo
    {
        private readonly KitchenContext _context;

        public KitchenRepo(KitchenContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<FridgeItem>> GetAllFridgeItemsAsync()
        {
            return await _context.FridgeItems.ToListAsync();
        }

        public Task<IEnumerable<FridgeItem>> GetOwnedFridgeItemsAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FridgeItem>> GetExpiredFridgeItemsAsync(DateTime cutoff)
        {
            return await _context.FridgeItems
                .Where(i => i.Expiration <= cutoff)
                .ToListAsync();
        }

        public async Task<FridgeItem> GetFridgeItemAsync(int id)
        {
            return await _context.FridgeItems.FindAsync(id);
        }

        public async Task<FridgeItem> CreateFridgeItemAsync(FridgeItem item)
        {
            await _context.FridgeItems.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public Task<bool> UpdateFridgeItemAsync(FridgeItem item)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteFridgeItemAsync(int id)
        {
            if (await _context.FridgeItems.FindAsync(id) is FridgeItem item)
            {
                _context.FridgeItems.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
