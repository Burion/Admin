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
using Admin.Decisions;
using Admin.ViewModels;
using Admin.ResultModels;
using Microsoft.AspNetCore.Identity;
using Musical_WebStore_BlazorApp.Server.Data.Models;
using Admin.Services;

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
        private readonly ServicesDecisionHendler _decisionHendler;
        private readonly IFileSavingService fileSavingService;
        public ManagementController(MusicalShopIdentityDbContext ctx, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ServicesDecisionHendler decisionHendler, IFileSavingService ifileSavingService)
        {
            this.ctx = ctx;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _decisionHendler = decisionHendler;
            fileSavingService = ifileSavingService;
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

        [Route("decisions/{city}")]
        public List<ServiceViewModel> Decision(string city)
        {
            var services = _decisionHendler.InitServices(city);
            return services.Select(s => _mapper.Map<ServiceViewModel>(s)).ToList();
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
        [Route("getuserinfo")]
        public async Task<ProfileModel> GetUserInfo()
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var profileModel = _mapper.Map<ProfileModel>(user);
            if(ctx.CompanyUsers.Select(cu => cu.UserId).Contains(user.Id))
            {
                var company = ctx.CompanyUsers.Single(cu => cu.UserId == user.Id).Company;
                profileModel.Company = _mapper.Map<CompanyModel>(company);
            }
            else if(ctx.ServiceUsers.Select(cu => cu.UserId).Contains(user.Id))
            {
                var service = ctx.ServiceUsers.Single(cu => cu.UserId == user.Id).Service;
                profileModel.Company = _mapper.Map<CompanyModel>(service);
            }
            else
            {

            }
            return profileModel;
        }

        [Route("getuserinfo/{userId}")]
        public async Task<ProfileModel> GetUserInfo(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var profileModel = _mapper.Map<ProfileModel>(user);
            if(ctx.CompanyUsers.Select(cu => cu.UserId).Contains(user.Id))
            {
                var company = ctx.CompanyUsers.Single(cu => cu.UserId == user.Id).Company;
                profileModel.Company = _mapper.Map<CompanyModel>(company);
            }
            else if(ctx.ServiceUsers.Select(cu => cu.UserId).Contains(user.Id))
            {
                var service = ctx.ServiceUsers.Single(cu => cu.UserId == user.Id).Service;
                profileModel.Company = _mapper.Map<CompanyModel>(service);
            }
            else
            {

            }
            return profileModel;
        }

        [Route("changepersinfo")]
        public async Task<Result> ChangePersInfo(PersonalInfo info)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var isValid = await _userManager.CheckPasswordAsync(user, info.OldPassword);
            if(isValid)
            {
                await _userManager.ChangePasswordAsync(user, info.OldPassword, info.NewPassword);
                return new Result() { Successful = true, Error = "Password was changed successfully" };
            }
            else
            {
                return new Result() { Successful = false, Error = "You entered wrong password" };
            }
        }
        [Route("changegeninfo")]
        public async Task<Result> ChangeGenInfo(EditGenUserInfo info)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            user.Name = info.Name;
            user.PhoneNumber = info.Phone;
            await _userManager.UpdateAsync(user);
            return new Result(){ Successful = true};
        }
        [Route("fireemp")]
        public async Task<Result> FireEmp(FireModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if(model.IsInService)
            {
                var su = ctx.ServiceUsers.Single(su => su.UserId == model.Id);
                ctx.ServiceUsers.Remove(su);
            }
            else
            {
                var cu = ctx.CompanyUsers.Single(cu => cu.UserId == model.Id);
                ctx.CompanyUsers.Remove(cu);
            }
            await ctx.SaveChangesAsync();
            return new Result(){ Successful = true};
        }
        [Route("hireemp")]
        public async Task<Result> HireEmp(UserLimited model)
        {
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            var isInService = ctx.ServiceUsers.Select(su => su.UserId).Contains(user.Id);
            var emp = await _userManager.FindByEmailAsync(model.Email);
            if(emp == null)
            {
                return new Result() {Successful = false, Error = $"There are no users registered with email {model.Email}" };
            }
            var alreadyHired = ctx.ServiceUsers.Select(su => su.UserId).Contains(emp.Id);
            if(alreadyHired) return new Result() { Successful = false, Error = $"{emp.Name} is already hired by another service"};
            alreadyHired = ctx.CompanyUsers.Select(su => su.UserId).Contains(emp.Id);
            if(alreadyHired) return new Result() { Successful = false, Error = $"{emp.Name} is already hired by another company"};
            if(isInService)
            {
                
                int serviceId = ctx.ServiceUsers.Single(su => su.UserId == user.Id).ServiceId;
                ctx.ServiceUsers.Add(new ServiceUser() { UserId = emp.Id, ServiceId = serviceId});
            }
            else
            {
                int companyId = ctx.CompanyUsers.Single(su => su.UserId == user.Id).CompanyId;
                ctx.CompanyUsers.Add(new CompanyUser() { UserId = emp.Id, CompanyId = companyId});
            }
            await ctx.SaveChangesAsync();
            return new Result(){ Successful = true, Error = $"{emp.Name} was hired successfully!"};
        }

        [Route("changeuserpicture")]
        public async Task<Result> ChangeUserPicture(AddUserPicture model)
        {
            var localFilePath = await SaveAndGetLocalFilePathIfNewerPhoto(model);
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            user.Image = localFilePath;
            await _userManager.UpdateAsync(user);
            return new Result(){ Successful = true };
        }
        private async Task<string> SaveAndGetLocalFilePathIfNewerPhoto(AddUserPicture model)
        {
            bool ValidateImage(AddUserPicture model) => model.ImageBytes != null && model.ImageType != null;

            var localFilePath =
                ValidateImage(model)
                ? await fileSavingService.SaveFileAsync(model.ImageBytes, model.ImageType, "images")
                : null;

            return localFilePath;
        }
    }
}

