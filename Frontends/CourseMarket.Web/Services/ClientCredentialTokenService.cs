using CourseMarket.Web.Models;
using CourseMarket.Web.Services.Interfaces;
using Duende.AccessTokenManagement;
using Duende.IdentityModel.Client;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CourseMarket.Web.Services
{
    public class ClientCredentialTokenService : IClientCredentialTokenService
    {
        private readonly ServiceApiSettings _serviceApiSettings;
        private readonly ClientSettings _clientSettings;
        private readonly IClientCredentialsTokenCache _clientCredentialsTokenCache;
        private readonly HttpClient _httpClient;

        public ClientCredentialTokenService(IOptions<ServiceApiSettings> serviceApiSettings, IOptions<ClientSettings> clientSettings, IClientCredentialsTokenCache clientCredentialsTokenCache, HttpClient httpClient)
        {
            _serviceApiSettings = serviceApiSettings.Value;
            _clientSettings = clientSettings.Value;
            _clientCredentialsTokenCache = clientCredentialsTokenCache;
            _httpClient = httpClient;
        }

        public async Task<string> GetToken()
        {
            var currentToken = await _clientCredentialsTokenCache.GetAsync("WebClientToken", new TokenRequestParameters());
            if (currentToken != null) return currentToken.AccessToken;

            var disco = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = _serviceApiSettings.IdentityBaseUri,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (disco.IsError) throw disco.Exception;

            var clientCredentialTokenRequest = new ClientCredentialsTokenRequest
            {
                ClientId = _clientSettings.WebClient.ClientId,
                ClientSecret = _clientSettings.WebClient.ClientSecret,
                Address = disco.TokenEndpoint
            };

            var newToken = await _httpClient.RequestClientCredentialsTokenAsync(clientCredentialTokenRequest);
            if(newToken.IsError) throw newToken.Exception;

            await _clientCredentialsTokenCache.SetAsync("WebClientToken", new ClientCredentialsToken { AccessToken = newToken.AccessToken, Expiration = DateTimeOffset.UtcNow.AddSeconds(newToken.ExpiresIn) }, new TokenRequestParameters());
            return newToken.AccessToken;
        }
    }
}
