
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreWeb.Core;
using StoreWeb.Core.Mapping.Products;
using StoreWeb.Core.Services.Contract;
using StoreWeb.Repository;
using StoreWeb.Repository.Data;
using StoreWeb.Repository.Data.Contexts;
using StoreWeb.Services.Services.Products;
using StoreWebApi.Errors;
using StoreWebApi.Helper;
using StoreWebApi.MiddleWares;

namespace StoreWebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDependency(builder.Configuration); 


            var app = builder.Build();

            await app.ConfigureMiddlewareAsync();
           

            app.Run();
        }
    }
}
