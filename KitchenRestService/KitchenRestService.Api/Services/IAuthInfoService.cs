using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace KitchenRestService.Api.Services
{
    public interface IAuthInfoService
    {
        Task<string> GetUserEmailAsync(HttpRequest currentRequest);
    }
}