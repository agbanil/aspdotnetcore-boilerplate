using Newtonsoft.Json;
using WebApi.Models.Avatar;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace WebApi.Services
{
    public interface IAvatarService
    {
        Task<AvatarResponse> GetByName(string name);
    }

    public class AvatarService : IAvatarService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger<AvatarService> _logger;

        public AvatarService(IHttpClientFactory clientFactory, ILogger<AvatarService> logger)
        {
            _clientFactory = clientFactory;
            _logger = logger;
        }

        public async Task<AvatarResponse> GetByName(string name)
        {
            _logger.LogInformation($"Getting avatar for user with username: {name}");
            var request = new HttpRequestMessage(HttpMethod.Get, $"{name}?json");
            var client = _clientFactory.CreateClient("unavatar");
            var clientResponse = await client.SendAsync(request);
            var res = "{'message': 'ok'}";
            if (clientResponse.IsSuccessStatusCode) {
                _logger.LogInformation($"Successfully retrieved avatar for user {name}");
                res = await clientResponse.Content.ReadAsStringAsync();
            } else {
                _logger.LogError($"There was an error getting avatar for user {name}: {clientResponse.ReasonPhrase}");
                throw new System.Exception($"There was an error getting avatar for user {name}: {clientResponse.ReasonPhrase}");
            }
            
            return JsonConvert.DeserializeObject<AvatarResponse>(res);
        }
    }
}