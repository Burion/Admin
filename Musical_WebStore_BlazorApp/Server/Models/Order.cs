using System;
using System.Collections.Generic;
using Musical_WebStore_BlazorApp.Server.Data.Models;

namespace Admin.Models
{
    public class Order: Entity
    {
        public DateTime Date {get;set;}
        public int ServiceId {get;set;}
        public virtual Service Service {get;set;}

        //user that ordered service
        public string UserId {get;set;}
        public virtual User User {get;set;}
        public string Text {get;set;}
        public virtual OrderType OrderType {get;set;}
        public int OrderTypeId {get;set;}
        public int DeviceId {get;set;}
        public virtual Device Device {get;set;}
        public int OrderStatusId {get;set;}
        public virtual OrderStatus OrderStatus {get;set;}
        public virtual IEnumerable<OrderWorker> OrderWorkers {get;set;}
    }
}