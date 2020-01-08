using System;

namespace Admin.ViewModels
{
    public class ModuleViewModel
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public LocationViewModel Location {get;set;}
        public string Type {get;set;}

    }
}