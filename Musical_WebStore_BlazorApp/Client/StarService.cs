using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Musical_WebStore_BlazorApp.Shared;

namespace Musical_WebStore_BlazorApp.Services
{
    public class StarService
    {
        private readonly HttpClient _httpClient;
        public StarService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<LeaveCommentResult> LeaveStar(StarModel model)
        {
            var result = await _httpClient.PostJsonAsync<LeaveCommentResult>("api/stars/addstar", model);
            return result;
        }

        public async Task<DeleteCommentResult> DeleteComment(DeleteStarModel model)
        {
            var result = await _httpClient.PostJsonAsync<DeleteCommentResult>("api/stars/deletestar", model);
            return result;
        }
        
    }
}