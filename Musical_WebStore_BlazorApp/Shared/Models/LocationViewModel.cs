using System;

namespace Admin.ViewModels
{

    public class LocationViewModel
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public string Address {get;set;}
        public int Status {get;set;}
        public string Image {get;set;}
        public string Description {get;set;}
        public AddUserPicture PictureModel {get;set;}
    }
}