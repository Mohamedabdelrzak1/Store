
using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using Persistence.Identity;
using Presentation.MiddeelWare;
using Service;
using Shared.Dto;
using Shared.ErrorModels;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Store.Api.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection RegisterAllServices(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddBuiltInService();
            services.AddSwaggerService();
            services.AddCustomIdentity(configuration); // ✅ بديل AddIdentityServices




            
            services.AddInfrastructureServices(configuration);
            services.AddApplicationServices(configuration);


            services.ConfigurService();





            return services;
        }

        private static IServiceCollection AddBuiltInService(this IServiceCollection services)
        {
            services.AddControllers();
          

            return services;
        }

        private static IServiceCollection AddCustomIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();

            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));


            services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequiredLength = 6;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<StoreIdentityDbContext>()
            .AddSignInManager<SignInManager<AppUser>>()
           .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey))
                };
            });

            services.AddAuthorization();

            return services;
        }
        private static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                // ✅ هذا السطر يحل التعارض بين الكلاسات المتشابهة بالاسم
                c.CustomSchemaIds(type => type.FullName);
            });



            return services;
        }
        private static IServiceCollection ConfigurService(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(config =>
            {

                config.InvalidModelStateResponseFactory = (actionContext) =>
                {

                    var errors = actionContext.ModelState.Where(m => m.Value.Errors.Any())
                                                 .Select(m => new ValidaionError()
                                                 {
                                                     Field = m.Key,
                                                     Errors = m.Value.Errors.Select(errors => errors.ErrorMessage)
                                                 });

                    var response = new ValidaionErrorResponse()
                    {
                        Errors = errors

                    };

                    return new BadRequestObjectResult(response);
                };


            });


            return services;
        }

        public static async Task<WebApplication> ConfigurMiddelwares(this WebApplication app)
        {

            await app.IntialiseDatabaseAsync();


            // Configure the HTTP request pipeline.

            app.UseGlobalErrorHandling();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            

            return app;
        }


        private static async Task<WebApplication> IntialiseDatabaseAsync(this WebApplication app)
        {

            //DataSeeding 
            using var Scoope = app.Services.CreateScope();

            var ObjectOfDataSeeding = Scoope.ServiceProvider.GetRequiredService<IDataSeeding>();

            await ObjectOfDataSeeding.DataSeedAsync();
            await ObjectOfDataSeeding.DataSeedIdentityAsync();


            return app;
        }

        private static WebApplication UseGlobalErrorHandling(this WebApplication app)
        {
            app.UseMiddleware<GlobalErrorHandlingMiddleWare>();

            return app;
        }


    }
}
     