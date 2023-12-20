using ApiOnion104.Application.MappingProfiles;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApiOnion104.Application.ServiceRegisteration
{
    public static class ServiceRegisteration
    {
        public  static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;

        }
    }
}
