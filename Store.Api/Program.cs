

using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Repositorys;
using Service;
using Service.MappingProfiles;
using ServiceAbstraction;
using Shared.ErrorModels;
using Store.Api.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace Store.Api
{
    public class Program
    {

        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.RegisterAllServices(builder.Configuration);



            var app = builder.Build();

            await app.ConfigurMiddelwares();

            app.Run();
        }
    }
}
