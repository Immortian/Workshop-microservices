using IdentityModel.Client;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    public class TokenService : ITokenService
    {
        public readonly IOptions<IdentityServerSettings> ISSettings;
        public readonly DiscoveryDocumentResponse discoveryDocument;
        private readonly HttpClient _httpClient;

        public TokenService(IOptions<IdentityServerSettings> iSSettings, HttpClient httpClient)
        {
            ISSettings = iSSettings;
            _httpClient = httpClient;
            discoveryDocument = _httpClient.GetDiscoveryDocumentAsync(this.ISSettings.Value.DiscoveryUrl).Result;

            if (discoveryDocument.IsError)
                throw new Exception("Unable to discovery document", discoveryDocument.Exception);
        }

        public async Task<TokenResponse> GetToken(string scope)
        {
            var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Address = discoveryDocument.TokenEndpoint,
                    ClientId = ISSettings.Value.ClientName,
                    ClientSecret = ISSettings.Value.ClientPassword,
                    Scope = scope
                });

            if (tokenResponse.IsError)
                throw new Exception("Unable to get token", tokenResponse.Exception);

            return tokenResponse;
        }
    }
}
