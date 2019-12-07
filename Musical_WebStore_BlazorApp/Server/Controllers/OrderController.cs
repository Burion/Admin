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
using Musical_WebStore_BlazorApp.Server.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Components.Authorization;
using Admin.Models;
using AutoMapper;
using Admin.ViewModels;
using Admin.ResultModels;

namespace Musical_WebStore_BlazorApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly MusicalShopIdentityDbContext ctx;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public OrderController(MusicalShopIdentityDbContext ctx, UserManager<User> userManager, IMapper mapper)
        {
            this.ctx = ctx;
            _userManager = userManager;
            _mapper = mapper;
        }
        private Task<Order[]> GetOrdersAsync() => ctx.Orders.ToArrayAsync();
        
        public Task<List<OrderType>> GetOrderTypesAsync() => ctx.OrderTypes.ToListAsync();
        [Route("getordertypes")]
        public async Task<List<OrderTypeViewModel>> GetOrderTypes()
        {
            var otvms = await GetOrderTypesAsync();
            var otvmViews = otvms.Select(ot => _mapper.Map<OrderTypeViewModel>(ot)).ToList();
            return otvmViews;
        }
        [Route("addorder")]
        public AddOrderResult AddOrder(AddOrderModel model)
        {
            var order = _mapper.Map<Order>(model);
            order.Date = DateTime.Now;
            order.UserId = _userManager.FindByEmailAsync(User.Identity.Name).Id;
            ctx.Orders.Add(order);
            ctx.SaveChanges();
            return new AddOrderResult() { Successful = true };
        }

    }
}