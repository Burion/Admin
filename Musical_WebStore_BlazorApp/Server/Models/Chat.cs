using System;
using System.Collections.Generic;

namespace Admin.Models
{
    public class Chat
    {
    public int Id {get;set;}
    public string Name {get;set;}
    public string Description {get;set;}
    public int ServiceId {get;set;}
    public virtual Service Service {get;set;}
    public int CompanyId {get;set;}
    public virtual Company Company {get;set;}
    public virtual ICollection<ChatUser> ChatUsers {get;set;}
    }


    
}