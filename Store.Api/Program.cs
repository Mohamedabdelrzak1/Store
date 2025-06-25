
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.Data;
using Persistence.Repositorys;
using Service;
using Service.MappingProfiles;
using ServiceAbstraction;
using System.Threading.Tasks;

namespace Store.Api
{
    public class Program
    {

        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            

            #region   Register or Add services to the container.


            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            
            builder.Services.AddDbContext<StoreDbContext>(option=>
            {


                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            }); //Allow DI  for SroreDbContext 

            builder.Services.AddScoped<IDataSeeding, DataSeeding>();   //Allow DI  for DataSeeding 
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();     //Allow DI  for UnitOfWork
            builder.Services.AddAutoMapper(typeof(Service.AssemblyReference).Assembly);
            builder.Services.AddScoped<IServiceManager, ServiceManager>(); //Allow DI  for ServiceManager
            #endregion


            var app = builder.Build();

            #region DataSeeding 
            using var Scoope = app.Services.CreateScope();

            var ObjectOfDataSeeding = Scoope.ServiceProvider.GetRequiredService<IDataSeeding>();

           await ObjectOfDataSeeding.DataSeedAsync(); 
            #endregion


            #region  Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers(); 
            #endregion

            app.Run();
        }
    }
}
