using System.Threading.Tasks;

namespace KitchenRestService.Logic
{
    public interface IUserRepo
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<User> CreateUserAsync(User user);
    }
}
