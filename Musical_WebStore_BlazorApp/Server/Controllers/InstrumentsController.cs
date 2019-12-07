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

namespace Musical_WebStore_BlazorApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstrumentsController : Controller
    {
        private readonly MusicalShopIdentityDbContext ctx;

        public InstrumentsController(MusicalShopIdentityDbContext ctx)
        {
            this.ctx = ctx;
        }

        private Task<Instrument[]> GetInstrumentsAsync() => ctx.Instruments.ToArrayAsync();

        [HttpGet]
        public async Task<IEnumerable<Instrument>> Get()
        {
            var instruments = await GetInstrumentsAsync();

            return instruments;
        }
    }
}

