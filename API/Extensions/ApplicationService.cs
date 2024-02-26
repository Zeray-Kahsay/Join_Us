using Application;
using Microsoft.EntityFrameworkCore;
using Persistent;

namespace API.Extensions
{
    public static class ApplicationService
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            //DbContext
            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            // Cors
            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
                });
            });
            // Register Mediator
            services.AddMediatR(config =>
                config.RegisterServicesFromAssembly(typeof(List.Handler).Assembly));
            // registers the Mediator found in Application project ONLY

            // auto mapper
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            return services;

        }
    }
}