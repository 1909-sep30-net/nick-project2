using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using KitchenRestService.Api.Models;
using Microsoft.AspNetCore.Http;

namespace KitchenRestService.Api.Services
{
    public class AuthInfoService : IAuthInfoService
    {
        private readonly HttpClient _client;

        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public AuthInfoService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<string> GetUserEmailAsync(HttpRequest currentRequest)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://escalonn.auth0.com/userinfo");
            var authHeader = currentRequest.Headers["Authorization"].ToString();
            request.Headers.Add("Authorization", authHeader);
            using HttpResponseMessage response = await _client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            using Stream responseStream = await response.Content.ReadAsStreamAsync();
            var info = await JsonSerializer.DeserializeAsync<AuthUserInfo>(responseStream, _jsonOptions);
            return info.Email;
        }
    }
}
