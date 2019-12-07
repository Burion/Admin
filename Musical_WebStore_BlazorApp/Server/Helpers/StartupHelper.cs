using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Musical_WebStore_BlazorApp.Server.Data;
using Musical_WebStore_BlazorApp.Server.Data.Models;
using Musical_WebStore_BlazorApp.Server.Helpers;
using System;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore.Proxies;

namespace Musical_WebStore_BlazorApp.Server.Helpers
{
    public static class StartupHelper
    {
        public static void EnsureDatabaseCreated<TContext>(IServiceProvider serviceProvider) where TContext : DbContext
        {
            using var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            using var context = scope.ServiceProvider.GetService<TContext>();
            context.Database.EnsureCreated();
        }

        public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                                .AddJwtBearer(options =>
                                {
                                    options.TokenValidationParameters = new TokenValidationParameters
                                    {
                                        ValidateIssuer = true,
                                        ValidateAudience = true,
                                        ValidateLifetime = true,
                                        ValidateIssuerSigningKey = true,
                                        ValidIssuer = configuration["JwtIssuer"],
                                        ValidAudience = configuration["JwtAudience"],
                                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSecurityKey"]))
                                    };
                                });
        }

        public static void AddCompression(this IServiceCollection services)
        {
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });
        }

        public static void AddIdentity(this IServiceCollection services)
        {
            services.AddDefaultIdentity<User>(options =>
            {
                options.User.RequireUniqueEmail = true;
            }).AddRoles<IdentityRole>()
              .AddEntityFrameworkStores<MusicalShopIdentityDbContext>()
              .AddDefaultTokenProviders();
        }

        public static void AddDatabaseProvider(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            var databaseType = configuration.GetDatabaseType();

            if (databaseType == DatabaseType.InMemory)
            {
                services.AddDbContext<MusicalShopIdentityDbContext>(
                    options => options
                     .UseLazyLoadingProxies()
                     .UseInMemoryDatabase("random_name"));
            }
            if (databaseType == DatabaseType.SQLSERVER)
            {
                var connStr = configuration.GetConnectionString(databaseType, env.IsDevelopment());

                services.AddDbContext<MusicalShopIdentityDbContext>(
                    options => options.UseSqlServer(connStr));
            }
        }

        public static string GetConnectionString(this IConfiguration configuration, DatabaseType type, bool isDev)
        {
            string dbName;

            if (isDev)
            {
                dbName = $"{(int)type}_AuthenticationDB_Local";
            }
            else
            {
                dbName = $"{(int)type}_AuthenticationDB";
            }

            return configuration.GetConnectionString(dbName);
        }
    }
}
