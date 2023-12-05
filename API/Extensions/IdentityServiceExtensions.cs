using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Services;
using Domain;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services , IConfiguration config){
            services.AddIdentityCore<AppUser>(op =>
            {
                op.Password.RequireNonAlphanumeric = false;
                op.Password.RequiredLength = 6;
            })
            
            .AddRoles<AppRol>()
            .AddEntityFrameworkStores<DataContext>();
            services.AddAuthentication();
            services.AddScoped<TokenService>();
            return services;
        }
    }
}