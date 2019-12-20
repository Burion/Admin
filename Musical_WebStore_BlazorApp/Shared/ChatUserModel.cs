using System;
using Admin.ViewModels;
using Musical_WebStore_BlazorApp.Shared;

namespace Admin.ViewModels
{
    public class ChatUserModel
    {
        public int ChatId {get;set;}
        public ChatModel Chat {get;set;}
        public string UserId {get;set;}
        public UserLimited User {get;set;}
        public string Position {get;set;}

    }
}