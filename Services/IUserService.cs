using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testeBlazor.Models;

namespace testeBlazor.Services
{
    public interface IUserService
    {
        public Task<User> LoginAsync(User user);
        public Task<User> RegisterUserAsync(User user);
        public Task<User> GetUserByAccessTokenAsync(string accessToken);
        public Task<User> RefreshTokenAsync(string refreshRequest);
    }
}
