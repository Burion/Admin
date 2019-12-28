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
    public class ManagementController : Controller
    {
        private readonly MusicalShopIdentityDbContext ctx;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ManagementController(MusicalShopIdentityDbContext ctx, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.ctx = ctx;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [Route("getservices")]
        public async Task<ServiceViewModel[]> GetServices()
        {
            return await ctx.Services.Select(s => _mapper.Map<ServiceViewModel>(s)).ToArrayAsync();
        }
        [Route("getcompanies")]
        public async Task<CompanyModel[]> GetCompanies()
        {
            return await ctx.Companies.Select(s => _mapper.Map<CompanyModel>(s)).ToArrayAsync();
        }

        [Route("getserviceusers/{serviceId}")]
        public async Task<UserLimited[]> GetServiceUsers(int serviceId)
        {
            return await ctx.ServiceUsers.Where(su => su.ServiceId == serviceId).Select(su => _mapper.Map<UserLimited>(su.User)).ToArrayAsync();
        }
        [Route("getcompanyusers/{companyId}")]
        public async Task<UserLimited[]> GetCompanyUsers(int companyId)
        {
            return await ctx.CompanyUsers.Where(cu => cu.CompanyId == companyId).Select(cu => _mapper.Map<UserLimited>(cu.User)).ToArrayAsync();
        }

        [Route("getserviceusers")]
        public async Task<UserLimited[]> GetServiceUsers()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            int serviceId = ctx.ServiceUsers.Single(su => su.UserId == user.Id).ServiceId;
            return await ctx.ServiceUsers.Where(su => su.ServiceId == serviceId).Select(su => _mapper.Map<UserLimited>(su.User)).ToArrayAsync();
        }

        [Route("getcompanyusers")]
        public async Task<UserLimited[]> GetCompanyUsers()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            int companyId = ctx.CompanyUsers.Single(cu => cu.UserId == user.Id).CompanyId;
            var us = await ctx.CompanyUsers.Where(cu => cu.CompanyId == companyId).Select(cu => _mapper.Map<UserLimited>(cu.User)).ToArrayAsync();
            foreach(var u in us)
            {
                var roles =await _userManager.GetRolesAsync(_userManager.FindByIdAsync(u.Id).Result);
                u.Role = roles.First();
            }
            return us;
        }
        [Route("getroles")]
        public async Task<RoleModel[]> GetRoles()
        {
            RoleModel[] roles;
            if(User.IsInRole("CompanyAdmin"))
            {
                roles = new  RoleModel[] {new RoleModel() { Name = "CompanyAdmin"}, new RoleModel() { Name = "CompanyOperator"}};
            }
            else if(User.IsInRole("ServiceAdmin"))
            {
                roles = new  RoleModel[] {new RoleModel() { Name = "ServiceAdmin"}, new RoleModel() { Name = "ServiceOperator"}, new RoleModel() { Name = "Worker"}};
            }
            else
            throw new Exception("Fuck you!");

            return roles;
        }

        [Route("editemp")]
        public async Task<Result> EditEmp(EditEmpModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            var userroles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, userroles);
            await _userManager.AddToRoleAsync(user, model.NewRole);
            var userroles1 = await _userManager.GetRolesAsync(user);
            user.Position = model.Position;
            await _userManager.UpdateAsync(user);
            return new Result() {Successful = true};
        }
        [Route("addorg")]
        public async Task<Result> AddService(AddOrgModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.OwnersEmail);
            switch(model.Type)
            {
                case "Service":
                    await _userManager.AddToRoleAsync(user, "ServiceAdmin");
                    var service = new Service() {CreationDate = DateTime.Now};
                    ctx.Services.Add(service);
                    await ctx.SaveChangesAsync();
                    ctx.ServiceUsers.Add(
                        new ServiceUser()
                        {
                            ServiceId = service.Id,
                            UserId = user.Id 
                        }
                    );
                    break;
                case "Company":
                    await _userManager.AddToRoleAsync(user, "CompanyAdmin");
                    var company = new Company() {CreationDate = DateTime.Now};
                    ctx.Companies.Add(company);
                    await ctx.SaveChangesAsync();
                    ctx.CompanyUsers.Add(
                        new CompanyUser()
                        {
                            CompanyId = company.Id,
                            UserId = user.Id 
                        }
                    );
                    break;
            }
            await ctx.SaveChangesAsync();
            return new Result() {Successful = true};
            //TODO constraints
        }
    }
}

