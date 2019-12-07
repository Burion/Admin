using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Musical_WebStore_BlazorApp.Shared
{
    public class CommentModel
    {
        public string Text { get; set; }
        public string AuthorId {get;set;}
        public int InstrumentId {get;set;}
    }
}
