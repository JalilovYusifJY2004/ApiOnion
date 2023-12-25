using ApiOnion104.Application.Abstractions.Repositories;
using ApiOnion104.Application.Abstractions.Services;
using ApiOnion104.Persistence.Contexts;
using ApiOnion104.Persistence.Implementations.Repositories;
using ApiOnion104.Persistence.Implementations.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiOnion104.Persistence.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("Default")));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepositoy, ProductRepository>();
            services.AddScoped<IColorRepositoy, ColorRepository>();


            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            return services;
        }
    }
}
