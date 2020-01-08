using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Musical_WebStore_BlazorApp.Server.Data.Models
{
    public class User : IdentityUser
    {
        public string Position {get;set;}
        public string Image {get;set;}
        public string Name {get;set;}
    }
}
