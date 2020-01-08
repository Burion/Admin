using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Admin.ViewModels
{
    public class ProfileModel
    {
        public string Email { get; set; }
        public string UserName {get;set;}
        public string Name {get;set;}
        public string Phone {get;set;}
        public CompanyModel Company {get;set;}
        public ServiceViewModel Service {get;set;}
        public string PhoneNumber {get;set;}
        public string Image {get;set;}
    }
}
