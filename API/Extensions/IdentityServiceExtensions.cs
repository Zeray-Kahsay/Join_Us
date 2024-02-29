using System.Text;
using API.Services;
using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Persistent;

namespace API.Extensions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.RequireUniqueEmail = true;

            })
            .AddEntityFrameworkStores<DataContext>(); // allows quering users from entity framework store

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(opt =>
                    {
                        opt.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = key,
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    });
            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("IsActivityHost", policy =>
                {
                    policy.Requirements.Add(new IsHotRequirement());
                });
            });
            services.AddTransient<IAuthorizationHandler, IsHotRequirementHandler>();

            services.AddScoped<TokenService>();

            return services;
        }
    }
}