using System;
using Musical_WebStore_BlazorApp.Server.Data.Models;

namespace Admin.Models
{
    public class ServiceUser
    {
        public string UserId {get;set;}
        public virtual User User {get;set;}
        public int ServiceId {get;set;}
        public virtual Service Service {get;set;}
    }
    
}