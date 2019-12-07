using System;
using System.Collections.Generic;
using System.Text;

namespace Musical_WebStore_BlazorApp.Shared
{
    public class EditGoodModel : AddGoodModel
    {
        public int Id { get; set; }
        public string ImageSrc { get; set; }
    }
}
