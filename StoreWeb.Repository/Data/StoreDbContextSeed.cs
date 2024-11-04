using StoreWeb.Core.Entities;
using StoreWeb.Core.Entities.Order;
using StoreWeb.Repository.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace StoreWeb.Repository.Data
{
    public static class StoreDbContextSeed
    {
        public static async Task SeedAsync(StoreDbContext _context)
        {
            // Brand
            if (_context.Brands.Count() == 0)
            {
                
                // 1. Read Data From Json File

                var brandsData = File.ReadAllText(@"..\StoreWeb.Repository\Data\DataSeed\brands.json");

                // 2. Convert Json String To List<T>

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                // 3. Seed Data To DB
                if (brands is not null && brands.Count() > 0)
                {
                    await _context.Brands.AddRangeAsync(brands);
                    await _context.SaveChangesAsync();
                }
            }

            // Types

            if(_context.Types.Count() == 0)
            {
                var TypesData = File.ReadAllText(@"..\StoreWeb.Repository\Data\DataSeed\types.json");

                var types=JsonSerializer.Deserialize<List<ProductType>>(TypesData);

                if(types is not null && types.Count() > 0)
                {
                    await _context.Types.AddRangeAsync(types);
                    await _context.SaveChangesAsync();
                } 

            }

            // Products
            if (_context.Products.Count() == 0)
            {
                var productData = File.ReadAllText(@"..\StoreWeb.Repository\Data\DataSeed\products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productData);

                if (products is not null && products.Count() > 0)
                {
                    await _context.Products.AddRangeAsync(products);
                    await _context.SaveChangesAsync();
                }

            }


            // Delivery Methods
            if (_context.DeliveryMethods.Count() == 0)
            {
                // 1. Read Data From Json File
                var DeliveryMethodsData = File.ReadAllText(@"..\StoreWeb.Repository\Data\DataSeed\delivery.json");
                // 2. Convert Json String To List<>
                var DeliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodsData);
                // 3. Send Data To DB
                if (DeliveryMethods is not null)
                {
                    await _context.DeliveryMethods.AddRangeAsync(DeliveryMethods);
                    await _context.SaveChangesAsync();
                }

            }


        }




    }
}
