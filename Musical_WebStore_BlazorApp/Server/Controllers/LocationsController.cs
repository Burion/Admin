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
using Admin.Services;

namespace Musical_WebStore_BlazorApp.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : Controller
    {
        private readonly MusicalShopIdentityDbContext ctx;
        private readonly IMapper _mapper;
        private readonly IFileSavingService fileSavingService;
        public LocationsController(MusicalShopIdentityDbContext ctx, IMapper mapper, IFileSavingService fileSavingService)
        {
            this.ctx = ctx;
            _mapper = mapper;
            this.fileSavingService = fileSavingService;
        }

        private Task<Location[]> GetLocationsAsync() => ctx.Locations.ToArrayAsync();

        [HttpGet]
        public async Task<IEnumerable<LocationViewModel>> Get()
        {
            var locations = await GetLocationsAsync();
            var locationsModels = locations.Select(l => _mapper.Map<LocationViewModel>(l)).ToArray();
            for(int x = 0; x < locationsModels.Count(); x++)
            {
                locationsModels[x].Status = x % 3 + 1;
            }
            return locationsModels;
        }

        public async Task<Device[]> GetSensorsAsync() => await ctx.Devices.ToArrayAsync();
        [Route("getdevices")]
        public async Task<IEnumerable<DeviceViewModel>> GetDevices()
        {
            var devices = await GetSensorsAsync();
            var deviceViewModels = devices.Select(dev => new DeviceViewModel(){ Id = dev.Id, Name = dev.Name});
            return deviceViewModels;
        }
        public async Task<Module[]> GetDevicesAsync() => await ctx.Modules.ToArrayAsync();
        [Route("getmodules/{locationId}")]
        public async Task<IEnumerable<ModuleViewModel>> GetSensors(int locationId)
        {
            var devices = await GetDevicesAsync();
            var deviceViewModels = devices.Where(d => d.LocationId == locationId).Select(dev => _mapper.Map<ModuleViewModel>(dev));
            return deviceViewModels;
        }

        [Route("getmeterings")]
        public async Task<Metering[]> GetMeteringsAsync() => await ctx.Meterings.ToArrayAsync();
        public async Task<IEnumerable<MeteringModel>> GetMeterings()
        {
            var meterings = await GetMeteringsAsync();
            var meteringsModels = meterings.Select(m => _mapper.Map<MeteringModel>(m));
            return meteringsModels;
        }

        [Route("editlocation")]
        public async Task<Result> EditLocation(LocationViewModel model)
        {
            var location = ctx.Locations.Single(l => l.Id == model.Id);
            location.Address = model.Address;
            location.Description = model.Description = model.Description;
            location.Name = model.Name;
            location.Image = await SaveAndGetLocalFilePathIfNewerPhoto(model.PictureModel);
            await ctx.SaveChangesAsync();
            return new Result() {Successful = true };
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

