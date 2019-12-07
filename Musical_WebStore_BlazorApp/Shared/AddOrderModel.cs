using System;

namespace Admin.ViewModels
{
    public class AddOrderModel
    {
        public int ServiceId {get;set;}
        public string UserId {get;set;}
        public string Text {get;set;}
        public string OrderTypeId {get;set;}
        public string DeviceId {get;set;}
    }
}