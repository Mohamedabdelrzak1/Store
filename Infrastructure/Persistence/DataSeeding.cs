using Domain.Contracts;
using Domain.Models;
using Domain.Models.Identity;
using Domain.Models.OrderModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext,
        StoreIdentityDbContext IdentitydbContext,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager) : IDataSeeding
    {


        public async Task DataSeedAsync()
        {


            // Create Database If it dosen't Exists && Apply To Any Pending Migrations 

            var PandingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();

            try
            {
                if (PandingMigrations.Any()) //     ولا لاء Applied بتشوف الداتا بيز كلا
                {
                    await _dbContext.Database.MigrateAsync();
                }

                // Data Seeding 


                // Seeding ProductTypes From Json Files
                if (!_dbContext.ProductBrands.Any())
                {

                    // 1. Read All Data From types Json File as String 
                    //Sync عادية 
                    //var ProductBrandData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\DataSeeding\brands.json");
                    var ProductBrandData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeeding\brands.json");

                    // 2. Transform String To C# Objects [List<ProductTypes>]
                    //Convert Data "String" =>  C# Objects [productBrands]

                    var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandData);


                    // 3.Add List<ProductTypes> To Database   => Save To Db
                    if (ProductBrands is not null && ProductBrands.Any())
                    {
                        await _dbContext.ProductBrands.AddRangeAsync(ProductBrands);
                        await _dbContext.SaveChangesAsync();
                    }
                        
                }

                // Seeding ProductBrands From Json Files 
                if (!_dbContext.ProductTypes.Any())
                {
                    // 1. Read All Data From types Json File as String 
                    //Sync عادية
                    //var ProductTypeData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\DataSeeding\types.json");
                    var ProductTypeData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeeding\types.json");


                    // 2. Transform String To C# Objects [List<ProductTypes>]
                    //Convert Data "String" =>  C# Objects [productTypes]

                    var ProductTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypeData);


                    // 3.Add List<ProductTypes> To Database   => Save To Db
                    if (ProductTypes is not null && ProductTypes.Any())
                    {
                        await _dbContext.ProductTypes.AddRangeAsync(ProductTypes);
                        await _dbContext.SaveChangesAsync();
                    }
                        
                }

                // Seeding Products From Json Files 

                if (!_dbContext.Products.Any())
                {
                    // 1. Read All Data From types Json File as String 
                    //Sync عادية
                    //var ProductData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\DataSeeding\products.json");
                    var ProductData = File.OpenRead(@"..\Infrastructure\Persistence\Data\DataSeeding\products.json");


                    // 2. Transform String To C# Objects [List<ProductTypes>]
                    //Convert Data "String" =>  C# Objects [productBrands]

                    var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductData);


                    // 3.Add List<ProductTypes> To Database   => Save To Db
                    if (Products is not null && Products.Any())
                    {
                        await _dbContext.Products.AddRangeAsync(Products);
                        await _dbContext.SaveChangesAsync();
                    }
                        
                } 


                // Seeding Delivery From Json File 

                if (!_dbContext.DeliveryMethods.Any())
                {
                    // 1. Read All Data Delivery From Types Json File 

                    var deliveryData = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Data\DataSeeding\delivery.json");

                    // 2. Transform String To C# Objects (List<DeliveryMethod>)

                    var delivery = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryData);

                    // 3. Add Data To Database

                    if (delivery is not null && delivery.Any())
                    {
                        await _dbContext.DeliveryMethods.AddRangeAsync(delivery);
                        await _dbContext.SaveChangesAsync();
                    }

                    

                }






            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ DbUpdateException: " + ex.Message);
                if (ex.InnerException != null)
                    Console.WriteLine("➡️ InnerException: " + ex.InnerException.Message);
                throw;
            }

        }

        public async Task DataSeedIdentityAsync()
        {
            // Create Database If it dosen't Exists && Apply To Any Pending Migrations 

            var PandingMigrations = await IdentitydbContext.Database.GetPendingMigrationsAsync();

           
            
                if (PandingMigrations.Any())
                {
                    await IdentitydbContext.Database.MigrateAsync();
                }

                // Data Seeding 
                if (roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole()
                    {
                        Name = "Admin"

                    });

                    await roleManager.CreateAsync(new IdentityRole()
                    {
                        Name = "SuperAdmin"

                    });

                }

                // Seeding 

                if (userManager.Users.Any())
                {
                    var superAdminUser = new AppUser()
                    {
                        DisplayName = "Super Admin",
                        Email = "SuperAdmin@gmail.com",
                        UserName = "SuperAdmin",
                        PhoneNumber = "01234567890"
                    };

                    var AdminUser = new AppUser()
                    {
                        DisplayName = "Admin",
                        Email = "Admin@gmail.com",
                        UserName = "Admin",
                        PhoneNumber = "01234567890"
                    };

                    await userManager.CreateAsync(superAdminUser, "P@ssW0rd");
                    await userManager.CreateAsync(AdminUser, "P@ssW0rd");

                   if (await roleManager.RoleExistsAsync("SuperAdmin"))
                    {
                        await userManager.AddToRoleAsync(superAdminUser, "SuperAdmin");
                    }

                    if (await roleManager.RoleExistsAsync("Admin"))
                    {
                        await userManager.AddToRoleAsync(superAdminUser, "Admin");
                    }
                await IdentitydbContext.SaveChangesAsync();


            }
        } 
         } 
    }


