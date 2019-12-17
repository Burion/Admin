using System;
namespace Admin.ViewModels
{
    public class ServicePageModel
    {
        public int Id {get;set;}
        public string Name {get;set;}
        public string Address {get;set;}
        public string Phone {get;set;}
        public int OrdersCount {get;set;}
        public int OrdersSucceedCount {get;set;}
        public int ReviewsCount {get;set;}
        public float AvarageMark {get;set;}
        public string About {get;set;}

    }
}