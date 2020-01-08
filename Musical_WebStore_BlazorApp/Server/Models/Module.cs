using System;
using System.Collections.Generic;

namespace Admin.Models
{
    public class Module: Entity
    {
        public string Name {get;set;}
        public int LocationId {get;set;}
        public virtual Location Location {get;set;}
        public virtual IEnumerable<Device> Devices {get;set;}
    }
}