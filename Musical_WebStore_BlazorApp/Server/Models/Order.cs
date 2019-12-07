using System;
using Musical_WebStore_BlazorApp.Server.Data.Models;

namespace Admin.Models
{
    public class Order: Entity
    {
        public DateTime Date {get;set;}
        public int ServiceId {get;set;}
        public virtual Service Service {get;set;}
        public int UserId {get;set;}
        public virtual User User {get;set;}
        public string Text {get;set;}
        public virtual OrderType OrderType {get;set;}
        public int OrderTypeId {get;set;}
        public int DeviceId {get;set;}
        public virtual Device Device {get;set;}
    }
}