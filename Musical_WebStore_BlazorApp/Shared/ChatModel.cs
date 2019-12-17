using System;
using System.Collections.Generic;

namespace Admin.ViewModels
{
    public class ChatModel
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public string Description {get;set;}
        public int CompanyId {get;set;}
        public CompanyModel Company {get;set;}
    }
    
}