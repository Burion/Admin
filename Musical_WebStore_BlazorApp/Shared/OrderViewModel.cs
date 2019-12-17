using System;
using System.Collections.Generic;
using Musical_WebStore_BlazorApp.Shared;

namespace Admin.ViewModels
{
    public class OrderViewModel
    {
        public int Id {get;set;}
        public DateTime Date {get;set;}
        public int ServiceId {get;set;}
        public ServiceViewModel Service {get;set;}
        public int UserId {get;set;}
        public virtual UserLimited User {get;set;}
        public string Text {get;set;}
        public virtual OrderTypeViewModel OrderType {get;set;}
        public int OrderTypeId {get;set;}
        public int DeviceId {get;set;}
        public ModuleViewModel Device {get;set;}
        public OrderStatusViewModel OrderStatus {get;set;}
        public IEnumerable<OrderWorkerModel> OrderWorkers {get;set;}
    }
}