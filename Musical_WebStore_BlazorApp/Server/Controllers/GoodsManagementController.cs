using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Musical_WebStore_BlazorApp.Server.Data;
using Musical_WebStore_BlazorApp.Server.Services;
using Musical_WebStore_BlazorApp.Shared;

namespace Musical_WebStore_BlazorApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class GoodsManagementController : ControllerBase
    {
        private readonly MusicalShopIdentityDbContext ctx;
        private readonly IFileSavingService fileSavingService;

        public GoodsManagementController(MusicalShopIdentityDbContext ctx, IFileSavingService fileSavingService)
        {
            this.ctx = ctx;
            this.fileSavingService = fileSavingService;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> DeleteGood(DeleteGoodModel m)
        {
            var instrument = new Instrument
            {
                Id = m.Id
            };

            ctx.Instruments.Attach(instrument);
            ctx.Instruments.Remove(instrument);

            await ctx.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> EditGood(EditGoodModel model)
        {
            Instrument instrument = await GetReadyToAddInstrument(model);
            instrument.Id = model.Id;
            instrument.Image ??= model.ImageSrc;

            var entity = await ctx.FindAsync<Instrument>(instrument.Id);

            if (entity == null)
            {
                throw new InvalidOperationException("null entity at editgood");
            }

            ctx.Entry(entity).CurrentValues.SetValues(instrument);

            await ctx.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public ValueTask<Instrument> GetGood(int id) => ctx.FindAsync<Instrument>(id);
           
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddGood(AddGoodModel model)
        {
            Instrument instrument = await GetReadyToAddInstrument(model);

            await ctx.Instruments.AddAsync(instrument);
            await ctx.SaveChangesAsync();

            return Ok();
        }

        private async Task<Instrument> GetReadyToAddInstrument(AddGoodModel model)
        {        
            var localFilePath = await SaveAndGetLocalFilePathIfNewerPhoto(model);
            
            var instrument = new Instrument
            {
                Description = model.Description,
                Quantity = model.Quantity,
                Price = model.Price,
                TypeName = model.TypeName,
                Title = model.Title,
                Image = localFilePath
            };

            return instrument;
        }

        private async Task<string> SaveAndGetLocalFilePathIfNewerPhoto(AddGoodModel model)
        {
            bool ValidateImage(AddGoodModel model) => model.ImageBytes != null && model.ImageType != null;

            var localFilePath =
                ValidateImage(model)
                ? await fileSavingService.SaveFileAsync(model.ImageBytes, model.ImageType, "images")
                : null;

            return localFilePath;
        }
    }
}