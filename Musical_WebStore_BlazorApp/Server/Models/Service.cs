using System;
using System.Collections.Generic;
using Musical_WebStore_BlazorApp.Server.Data.Models;

namespace Admin.Models
{   
    public class Service: Entity
    {
    public string Name {get;set;}
    public string Image {get;set;}
    public string Address {get;set;}
    public string Phone {get;set;}
    public virtual IEnumerable<ServiceUser> ServiceUsers {get;set;} 

    }
}