using System;
using Musical_WebStore_BlazorApp.Server.Data.Models;

namespace Admin.Models
{
    public class Review: Entity
    {

        public string UserId {get;set;}
        public virtual User User {get;set;}
        public int CompanyId {get;set;}
        public virtual Company Company {get;set;}
        public int ServiceId {get;set;}
        public virtual Service Service {get;set;}
        public string Text {get;set;}
        public int Mark {get;set;}
    }
}