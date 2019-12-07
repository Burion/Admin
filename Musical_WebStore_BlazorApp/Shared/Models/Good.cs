using System;
using System.Collections.Generic;
using System.Text;

namespace Musical_WebStore_BlazorApp.Shared
{
    public abstract class Good
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public string Image { get; set; } 
        
        public string TypeName { get; set; }

        public Good()
        {
            TypeName = this.GetType().Name;
        }
    }   
}
