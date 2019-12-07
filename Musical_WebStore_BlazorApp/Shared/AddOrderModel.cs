using System;

namespace Admin.ViewModels
{
    public class AddOrderModel
    {
        public int ServiceId {get;set;}
        public int UserId {get;set;}
        public string Text {get;set;}
        public int OrderTypeId {get;set;}
        public int DeviceId {get;set;}
    }
}