using AutoMapper;
using Course.Domain.Mappings;
using Course.Infrastructure.DbContexts;
using Course.Infrastructure.Interfaces;
using Course.Infrastructure.Repository;
using Course.Infrastructure.service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Course.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IUserService, UserService>();
            IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
            services.AddSingleton(mapper);
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseDbContext, CourseDbContext>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}
