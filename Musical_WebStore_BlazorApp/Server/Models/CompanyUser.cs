using System;
using Musical_WebStore_BlazorApp.Server.Data.Models;

namespace Admin.Models
{
    public class CompanyUser
    {
        public int CompanyId {get;set;}
        public virtual Company Company {get;set;}
        public string UserId {get;set;}
        public virtual User User {get;set;}    
    }
}