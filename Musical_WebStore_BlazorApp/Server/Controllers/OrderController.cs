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

        [Route("getorders")]
        public async Task<List<OrderViewModel>> Get()
        {
            return await Get(User.Identity.Name);
            var orders = await GetOrdersAsync();
            var orderVM = orders.Select(o => _mapper.Map<OrderViewModel>(o)).ToList();
            return orderVM;
        }

        [Route("getorders/{email}")]
        public async Task<List<OrderViewModel>> Get(string email)
        {
            var orders = await GetOrdersAsync();
            var user = await _userManager.FindByEmailAsync(email);

            if(_userManager.IsInRoleAsync(user, "Worker").Result)
                orders = ctx.OrderWorkers
                .Where(ow => ow.UserId == user.Id)
                .Select(ow => ctx.Orders.Single(o => o.Id == ow.OrderId))
                .ToArray();
            else if(_userManager.IsInRoleAsync(user, "ServiceOperator").Result)
                orders = orders.Where(
                    o => o.ServiceId == ctx.Services.Single(
                        s => s.Id == ctx.ServiceUsers.Single(su => su.UserId == user.Id).ServiceId).Id
                ).ToArray();
            else if(_userManager.IsInRoleAsync(user, "CompanyOperator").Result)
            {
                var companyusersId = ctx.CompanyUsers.Where(
                    _cu => _cu.CompanyId == (ctx.CompanyUsers.Single(cu => cu.UserId == user.Id).CompanyId))
                    .Select(cu => cu.UserId)
                    .ToArray();
                orders = orders.Where(o => companyusersId.Contains(o.UserId)).ToArray();
            }
            var orderVM = orders.Select(o => _mapper.Map<OrderViewModel>(o)).ToList();
            return orderVM;
        }
        
        public Task<List<OrderType>> GetOrderTypesAsync() => ctx.OrderTypes.ToListAsync();
        [Route("getordertypes")]
        public async Task<List<OrderTypeViewModel>> GetOrderTypes()
        {
            var otvms = await GetOrderTypesAsync();
            var otvmViews = otvms.Select(ot => _mapper.Map<OrderTypeViewModel>(ot)).ToList();
            return otvmViews;
        }

        [Route("getorderstatuses")]
        public async Task<List<OrderStatusViewModel>> GetOrderStatuses()
        {
            return await ctx.OrderStatuses.Select(os => _mapper.Map<OrderStatusViewModel>(os)).ToListAsync();
        }
        [Route("addorder")]
        public AddOrderResult AddOrder(AddOrderModel model)
        {
            var order = _mapper.Map<Order>(model);
            order.Date = DateTime.Now;
            order.UserId = _userManager.FindByEmailAsync(User.Identity.Name).Result.Id;
            order.OrderStatusId = -1;
            order.ServiceId = -1; 
            ctx.Orders.Add(order);
            ctx.SaveChanges();
            return new AddOrderResult() { Successful = true };
        }
        [Route("addworkertoorder")]
        public async Task<Result> AddWorkerToOrder(AddWorkerToOrderModel model)
        {
            var orderworker = _mapper.Map<OrderWorker>(model);
            await ctx.OrderWorkers.AddAsync(orderworker);
            await ctx.SaveChangesAsync();
            return new Result() {Successful = true};
        }
        [Route("getworkers")]
        public async Task<UserLimited[]> GetWorkers()
        {
            var currentUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            var ServiceId = ctx.ServiceUsers.Single(su => su.UserId == currentUser.Id).ServiceId;
            var workers = await ctx.ServiceUsers
            .Where(su => su.ServiceId == ServiceId)
            .Where(su => _userManager.IsInRoleAsync(su.User, "Worker").Result)
            .Select(su => _mapper.Map<UserLimited>(su.User))
            .ToArrayAsync();
            return workers;
        }

        [Route("getworkers/{orderId}")]
        public async Task<UserLimited[]> GetWorkers(int orderId)
        {
            var currentUser = await _userManager.FindByEmailAsync(User.Identity.Name);
            var ServiceId = ctx.ServiceUsers.Single(su => su.UserId == currentUser.Id).ServiceId;
            var workers = await ctx.ServiceUsers
            .Where(su => su.ServiceId == ServiceId)
            .Where(su => !ctx.OrderWorkers.Where(ow => ow.OrderId == orderId).Select(ow => ow.UserId).Contains(su.UserId))
            .Select(su => _mapper.Map<UserLimited>(su.User))
            .ToArrayAsync();
            return workers;
        }

        [Route("editorderstatus")]
        public EditOrderStatusResult EditOrderStatus(EditOrderStatusModel model)
        {
            ctx.Orders.Single(o => o.Id == int.Parse(model.Id)).OrderStatusId = int.Parse(model.OrderStatusId);
            ctx.SaveChanges();
            return new EditOrderStatusResult() { Success = true };
        }

    }
}