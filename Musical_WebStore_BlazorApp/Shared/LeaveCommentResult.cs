using System;
using System.Collections.Generic;
using System.Text;

namespace Musical_WebStore_BlazorApp.Shared
{
    public class LeaveCommentResult
    {
        public bool Successful { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public LeaveCommentResult()
        {
            
        }
    }
}
