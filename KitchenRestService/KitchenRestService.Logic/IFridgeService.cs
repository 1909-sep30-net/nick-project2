using System.Threading.Tasks;

namespace KitchenRestService.Logic
{
    public interface IFridgeService
    {
        Task<bool> CleanFridgeAsync();
    }
}
