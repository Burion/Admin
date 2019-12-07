using System;
using System.Collections.Generic;
using System.Text;

namespace Musical_WebStore_BlazorApp.Server.Data.Models
{
    public class Comment
    {
        public int Id {get;set;}
        public string Text {get;set;}
        public DateTime Date {get;set;}
        public int InstrumentId {get;set;}
        public virtual User User {get;set;}
    }
}
