using System;
using Musical_WebStore_BlazorApp.Server.Data.Models;

namespace Admin.Models
{
    public class OrderWorker
    {
        public string UserId {get;set;}
        public virtual User User {get;set;}
        public int OrderId {get;set;}
        public virtual Order Order {get;set;}
    }
    
}