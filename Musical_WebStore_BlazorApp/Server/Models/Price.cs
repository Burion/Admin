using System;

namespace Admin.Models
{
    public class Price: Entity
    {
        public int ServiceId {get;set;}
        public Service Service {get;set;}
        public int OrderTypeId {get;set;}
        public OrderType OrderType {get;set;}
        public float Value {get;set;}
    }
}