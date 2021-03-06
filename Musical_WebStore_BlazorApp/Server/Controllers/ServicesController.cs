using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Musical_WebStore_BlazorApp.Shared;
using Musical_WebStore_BlazorApp.Client;
using Musical_WebStore_BlazorApp.Server.Data;
using Microsoft.EntityFrameworkCore;
using Admin.Models;
using AutoMapper;
using Admin.ViewModels;
using Admin.ResultModels;
using Microsoft.AspNetCore.Identity;
using Musical_WebStore_BlazorApp.Server.Data.Models;

namespace Musical_WebStore_BlazorApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : Controller
    {
        private readonly MusicalShopIdentityDbContext ctx;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ServicesController(MusicalShopIdentityDbContext ctx, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.ctx = ctx;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        
        [HttpGet]
        public async Task<ServicePageModel[]> Get()
        {
            return await ctx.Services.Select(s => _mapper.Map<ServicePageModel>(s)).ToArrayAsync();
        }

        [Route("getservices/{serviceId}")]
        public async Task<ServicePageModel> Get(string serviceId)
        {
            var model = _mapper.Map<ServicePageModel>(ctx.Services.Single(s => s.Id == int.Parse(serviceId)));
            var orders = ctx.Orders.Where(o => o.ServiceId == int.Parse(serviceId));
            if(orders.Count() != 0)
            {
                model.OrdersCount = orders.Count();
                model.OrdersSucceedCount = ctx.Orders.Where(o => o.ServiceId == int.Parse(serviceId)).Where(o => o.OrderStatus.Name == "Successfully Processed").Count();
            }
            else
            {
                model.OrdersCount = 0;
                model.OrdersSucceedCount = 0;
            }
            return model;
        }
    }
}

