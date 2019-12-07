using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Musical_WebStore_BlazorApp.Shared;

namespace Musical_WebStore_BlazorApp.Services
{
    public class CommentService
    {
        private readonly HttpClient _httpClient;
        public CommentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<LeaveCommentResult> LeaveComment(CommentModel model)
        {
            var result = await _httpClient.PostJsonAsync<LeaveCommentResult>("api/comments/addcomment", model);
            return result;
        }

        public async Task<DeleteCommentResult> DeleteComment(DeleteCommentModel model)
        {
            var result = await _httpClient.PostJsonAsync<DeleteCommentResult>("api/comments/deletecomment", model);
            return result;
        }
        
    }
}