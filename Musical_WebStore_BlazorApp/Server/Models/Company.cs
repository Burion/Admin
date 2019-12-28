using System;
using System.Collections.Generic;

namespace Admin.Models
{
    public class Company: Entity
    {
        public string Name {get;set;}
        public string Image {get;set;}
        public DateTime CreationDate {get;set;}

        public virtual IEnumerable<CompanyUser> CompanyUsers {get;set;}
    }
}