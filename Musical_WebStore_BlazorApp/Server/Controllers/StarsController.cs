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

namespace Musical_WebStore_BlazorApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StarsController : Controller
    {
        private readonly MusicalShopIdentityDbContext ctx;
        private readonly UserManager<User> _userManager;
        public StarsController(MusicalShopIdentityDbContext ctx, UserManager<User> userManager)
        {
            this.ctx = ctx;
            _userManager = userManager;
        }

        private Task<Star[]> GetStarsAsync() => ctx.Stars.ToArrayAsync();
        [HttpGet]
        public async Task<IEnumerable<Star>> Get()
        {
            var stars = await GetStarsAsync();

            return stars;
        }

        [Route("addstar")]
        public async Task<IActionResult> LeaveStar(StarModel model)
        {            
            
            var user = await _userManager.FindByEmailAsync(model.AuthorId);
            var str = ctx.Stars.SingleOrDefault(s => s.AuthorId == user.Id && s.InstrumentId == model.InstrumentId);
            if(str != null)
            {
                str.Mark = model.Mark;
            }
            else
            {
                ctx.Stars.Add(
                    new Star()
                    {
                        AuthorId = user.Id,
                        InstrumentId = model.InstrumentId,
                        Mark = model.Mark
                    }
                );
            }
            await ctx.SaveChangesAsync();
            return Ok(new StarResult(){Successful = true});
        }

        [Route("deletestar")]
        public async Task<IActionResult> DeleteStar(DeleteStarModel model)
        {            
            
            ctx.Stars.Remove(
                ctx.Stars.Single(c => c.Id == model.StarId)
            );
            await ctx.SaveChangesAsync();
            return Ok(new DeleteStarResult(){Successful = true});
        }
    }
}