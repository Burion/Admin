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
    public class ReviewsController : Controller
    {
        private readonly MusicalShopIdentityDbContext ctx;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public ReviewsController(MusicalShopIdentityDbContext ctx, IMapper mapper, UserManager<User> userManager)
        {
            this.ctx = ctx;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ReviewModel[]> Get()
        {
            return await ctx.Reviews.Select(r => _mapper.Map<ReviewModel>(r)).ToArrayAsync();
        }

        [Route("getreviews/{serviceId}")]
        public async Task<ReviewModel[]> Get(string serviceId)
        {
            return await ctx.Reviews.Where(r => r.ServiceId == int.Parse(serviceId)).Select(r => _mapper.Map<ReviewModel>(r)).ToArrayAsync();
        }

        [Route("addreview")]
        public async Task<Result> AddReview(AddReviewModel model)
        {
            var r = _mapper.Map<Review>(model);
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            r.UserId = user.Id;
            r.CompanyId = ctx.CompanyUsers.Single(cu => cu.UserId == user.Id).CompanyId;
            ctx.Reviews.Add(r);
            await ctx.SaveChangesAsync();
            return new Result() { Successful = true };
        }
    }
}

