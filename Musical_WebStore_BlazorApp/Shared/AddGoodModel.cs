using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Musical_WebStore_BlazorApp.Shared
{
    public class AddGoodModel
    {
        public string Title { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public string ImageBytes { get; set; }
        public string ImageType { get; set; }

        public string TypeName { get; set; }
    }
}
