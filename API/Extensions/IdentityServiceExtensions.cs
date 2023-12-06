
using System.Text;
using API.Services;
using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>{
                    opt.TokenValidationParameters= new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("x77qwsLXq9yKxJGLbF7ZnESXZ4AwsGXwmzugaPwx5VmFanAew6aH7EXRKFDGnLaE")) ,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                       
                      
                    };
                }
                
                );
            services.AddScoped<TokenService>();
            return services;
        }
    }
}