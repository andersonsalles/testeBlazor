using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using testeBlazor.Models;
using testeBlazor.Pages;

namespace testeBlazor.Services
{
    public class UserService : IUserService
    {
        public HttpClient _httpClient { get; }

        public UserService(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://localhost:44375");
            //httpClient.DefaultRequestHeaders.Add("User-Agent", "BlazorServer");

            _httpClient = httpClient;
        }

        public async Task<string> LoginAsync(User user)
        {
            string serializedUser = JsonConvert.SerializeObject(user);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/auth/login");
            requestMessage.Content = new StringContent(serializedUser);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            if (responseStatusCode == HttpStatusCode.OK)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var data = (JObject)JsonConvert.DeserializeObject(responseBody);
                string tk = data["token"].Value<string>();
                return tk;
            }

            return "";
        }

        public async Task<User> RegisterUserAsync(User user)
        {
            string serializedUser = JsonConvert.SerializeObject(user);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/auth/register");
            requestMessage.Content = new StringContent(serializedUser);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            if (responseStatusCode != HttpStatusCode.Created )
            {
                return null;
            }
            var responseBody = await response.Content.ReadAsStringAsync();


            return new User();
        }

        public Task<User> GetUserByAccessTokenAsync(string accessToken)
        {
            throw new NotImplementedException();
        }

        public Task<User> RefreshTokenAsync(string refreshRequest)
        {
            throw new NotImplementedException();
        }
    }
}
