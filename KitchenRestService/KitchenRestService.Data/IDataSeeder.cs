using System.Threading.Tasks;

namespace KitchenRestService.Data
{
    public interface IDataSeeder
    {
        Task<bool> SeedDataAsync();
    }
}