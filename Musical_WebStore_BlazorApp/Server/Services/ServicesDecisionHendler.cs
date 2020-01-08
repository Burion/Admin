using System;
using System.Collections.Generic;
using System.Linq;
using Admin.Helpers;
using Admin.Models;
using Musical_WebStore_BlazorApp.Server.Data;

namespace Admin.Decisions
{
    public class ServicesDecisionHendler
    {
        readonly MusicalShopIdentityDbContext _ctx;
        public List<Criterion> Criterions {get;set;} = new List<Criterion>();
        public List<Service> Services {get;set;}
        public Dictionary<Service, Dictionary<Criterion, float>> MarkSets = new Dictionary<Service, Dictionary<Criterion, float>>();
        public ServicesDecisionHendler(MusicalShopIdentityDbContext ctx)
        {
            _ctx = ctx;
            foreach(var enumcrit in Enum.GetValues(typeof(CriterionsEnum)))
            {
                Criterions.Add(new Criterion() { Name = enumcrit.ToString(), IsMax = (int)enumcrit > 0 ? true : false, Id = (int)enumcrit });
            }
        }

        public List<Service> InitServices(string City)
        {
            Services = _ctx.Services.Where(s => s.City == City).ToList();
            System.Random r = new System.Random();
            foreach(var s in Services)
            {
                MarkSets[s] = new Dictionary<Criterion, float>();
                foreach(var crit in Criterions)
                {
                    // MarkSets[s][crit] = GetValueFor(s, crit);    
                    MarkSets[s][crit] = r.Next(40, 100);    
                }
            }
            Normalize();
            Dictionary<Service, float> mins = new Dictionary<Service, float>();
            foreach(var ms in MarkSets)
            {
                var min = (float)ms.Value.Values.Min();
                mins[ms.Key] = min;
            }
            return mins.OrderByDescending(ms => ms.Value).Select(ms => ms.Key).ToList();
        }
        private float GetValueFor(Service service, Criterion critEmum)
        {
            float result = 0f;
            switch(critEmum.Id)
            {
                case (int)CriterionsEnum.AvgMark:
                result = (float)_ctx.Reviews.Where(r => r.ServiceId == service.Id).Average(r => r.Mark);
                    break;
                case (int)CriterionsEnum.FailedOrders:
                result = (float)_ctx.Orders.Where(o => o.ServiceId == service.Id).Count(o => o.OrderStatusId == -5);
                    break;
                case (int)CriterionsEnum.SucceedOrders:
                result = (float)_ctx.Orders.Where(o => o.ServiceId == service.Id).Count(o => o.OrderStatusId == -4);
                    break;
            }
            return result;
        }
        private void Normalize()
        {
            Dictionary<Criterion, float> NormCoefs = new Dictionary<Criterion, float>();
            if(MarkSets.Count() == 0)
            {
                throw new Exception("MarkSets are empty");
            }
            foreach(Criterion crit in Criterions)
            {
                List<float> values = new List<float>();
                foreach(var d in MarkSets.Values)
                {
                    var v = d.Single(dic => dic.Key == crit).Value;
                    if(v == 0)
                        v = 1;
                        //throw new DivideByZeroException(); 
                    values.Add(v);
                }
                var coef = values.Sum(v => 1/v);
                NormCoefs[crit] = coef;
            }
            foreach(var serv in Services)
            {
                foreach(var nc in NormCoefs)
                {
                    MarkSets[serv][nc.Key] *= nc.Value; 
                }
            }
        }
    }
}