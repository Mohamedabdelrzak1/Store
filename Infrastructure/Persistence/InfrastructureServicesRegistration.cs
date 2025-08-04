using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Identity;
using Persistence.Repositorys;
using Service;
using ServiceAbstraction;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {


            services.AddDbContext<StoreDbContext>(option =>
            {


                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            }); //Allow DI  for SroreDbContext 


            services.AddDbContext<StoreIdentityDbContext>(option =>
            {


                option.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));

            }); //Allow DI  for StoreIdentityDbContext 


            // تسجيل باقي الخدمات
            services.AddScoped<IDataSeeding, DataSeeding>();   //Allow DI  for DataSeeding 
            services.AddScoped<IUnitOfWork, UnitOfWork>();     //Allow DI  for UnitOfWork
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICacheRepository, CacheRepository>();

            // Redis
            var redisConnection = configuration.GetConnectionString("Redis");
            if (!string.IsNullOrWhiteSpace(redisConnection))
            {
                services.AddSingleton<IConnectionMultiplexer>(_ =>
                    ConnectionMultiplexer.Connect(redisConnection));
            }


            return services;
        }
    }
}

