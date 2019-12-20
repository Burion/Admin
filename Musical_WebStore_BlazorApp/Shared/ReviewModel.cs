using System;
using Musical_WebStore_BlazorApp.Shared;

namespace Admin.ViewModels
{
    public class ReviewModel
    {
        public string UserId {get;set;}
        public UserLimited User {get;set;}
        public int CompanyId {get;set;}
        public CompanyModel Company {get;set;}
        public int ServiceId {get;set;}
        public ServiceViewModel Service {get;set;}
        public string Text {get;set;}
        public string Mark {get;set;}
    }
}