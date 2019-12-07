using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Musical_WebStore_BlazorApp.Shared;

namespace Musical_WebStore_BlazorApp.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<EditUserResult> ChangeData(ProfileModel model)
        {
            var result = await _httpClient.PostJsonAsync<EditUserResult>("api/accounts/edit", model);
            return result;
        }
        
    }
}