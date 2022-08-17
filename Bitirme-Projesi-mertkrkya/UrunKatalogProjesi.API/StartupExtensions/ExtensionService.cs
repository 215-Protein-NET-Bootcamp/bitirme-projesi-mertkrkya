using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UrunKatalogProjesi.Core.Models;
using UrunKatalogProjesi.Data.Context;

namespace UrunKatalogProjesi.API.StartupExtensions
{
    public static class ExtensionService
    {
        public static void AddContextDependencyInjection(this IServiceCollection services, IConfiguration Configuration)
        {
            // cors 
            services.AddCors(options => options.AddPolicy("Cors", builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            AppDbContext.SetContextConnectionString(Configuration.GetConnectionString("DefaultConnection"));

            // identity
            services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();


            // identity options
            services.Configure<IdentityOptions>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 1;
            });
        }
    }
}
