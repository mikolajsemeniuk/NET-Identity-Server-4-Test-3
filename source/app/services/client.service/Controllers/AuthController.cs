using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace client.service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var credentials = new ClientCredentialsTokenRequest
            {
                Address = "https://localhost:5001/connect/token",
                // GrantType = "client_credentials",
                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api1"
            };

            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (disco.IsError)
                return null;

            var token = await client.RequestClientCredentialsTokenAsync(credentials);
            if (token.IsError)
                return null;

            var apiClient = new HttpClient();
            apiClient.SetBearerToken(token.AccessToken);
            
            var response = await apiClient.GetAsync("https://localhost:6001/api/customer");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            return Ok(content);
        }
    }
}
