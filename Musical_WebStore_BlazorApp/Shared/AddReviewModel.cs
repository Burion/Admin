using System;
using Musical_WebStore_BlazorApp.Shared;

namespace Admin.ViewModels
{
    public class AddReviewModel
    {
        public int ServiceId {get;set;}
        public string Text {get;set;}
        public string Mark {get;set;}
    }
}