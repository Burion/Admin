using System;
using Musical_WebStore_BlazorApp.Shared;

namespace Admin.ViewModels
{
    public class OrderWorkerModel
    {
        public string UserId {get;set;}
        public UserLimited User {get;set;}
        public int OrderId {get;set;}
        public OrderViewModel Order {get;set;}
    }
    
}