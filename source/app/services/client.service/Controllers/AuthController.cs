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
        private readonly ILogger _logger;

        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var credentials = new ClientCredentialsTokenRequest
            {
                Address = "https://localhost:5001/connect/token",
                GrantType = "client_credentials",
                ClientId = "client",
                ClientSecret = "secret",
                Scope = "api1"
            };

            var client = new HttpClient();

            var discovery = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
            if (discovery.IsError)
                return null;

            var token = await client.RequestClientCredentialsTokenAsync(credentials);
            if (token.IsError)
                return null;

            var apiClient = new HttpClient();
            apiClient.SetBearerToken(token.AccessToken);
            var response = await apiClient.GetAsync("http://localhost:6000/api/customer");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();


            // var httpClient = new HttpClient();
            // httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

            // // var json = JsonConvert.SerializeObject(command,
            // //     Formatting.None, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });

            // httpClient.DefaultRequestHeaders.Authorization = 
            //     new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.AccessToken);

            // // var content = new StringContent(json, Encoding.UTF8, "application/json");

            // var response = await httpClient.GetAsync("http://localhost:6000/api/customer");

            // response.EnsureSuccessStatusCode();
            // var content = await response.Content.ReadAsStringAsync();

            _logger.LogError($"check: {content}");

            return Ok();
        }
    }
}
