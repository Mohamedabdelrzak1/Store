using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {


        public async Task DataSeedAsync()
        {


            // Create Database If it dosen't Exists && Apply To Any Pending Migrations 

            var PandingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();

            try
            {
                if (PandingMigrations.Any()) //     ولا لاء Applied بتشوف الداتا بيز كلا
                {
                   await  _dbContext.Database.MigrateAsync();
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

                       await _dbContext.ProductBrands.AddRangeAsync(ProductBrands);
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

                      await  _dbContext.ProductTypes.AddRangeAsync(ProductTypes);
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

                    var Products =   await JsonSerializer.DeserializeAsync<List<Product>>(ProductData);


                    // 3.Add List<ProductTypes> To Database   => Save To Db
                    if (Products is not null && Products.Any())

                        await _dbContext.Products.AddRangeAsync(Products);
                }

                await _dbContext.SaveChangesAsync();

            } 
            catch(Exception ex)
            {
            //TODO
            }
            
        }
    }
}
