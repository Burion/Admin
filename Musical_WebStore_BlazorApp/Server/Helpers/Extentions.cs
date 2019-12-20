using System;
using System.Linq;
using Admin.Models;
using Musical_WebStore_BlazorApp.Server.Data;
using Musical_WebStore_BlazorApp.Server.Data.Models;

namespace Admin.Extentions
{
    public static class ExtentionClasses
    {
        public static Company GetCompany(this User user, MusicalShopIdentityDbContext ctx)
        {
            return ctx.CompanyUsers.Single(cu => cu.UserId == user.Id).Company;
        }
        public static Service GetService(this User user, MusicalShopIdentityDbContext ctx)
        {
            return ctx.ServiceUsers.Single(cu => cu.UserId == user.Id).Service;
        }
    }    
}