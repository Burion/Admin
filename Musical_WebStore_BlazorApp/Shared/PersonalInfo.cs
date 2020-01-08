using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Admin.ViewModels
{
    public class PersonalInfo
    {
        public string OldPassword {get;set;}
        public string NewPassword {get;set;}
    }
}
