using StoreWeb.Repository.Data.Contexts;
using StoreWeb.Repository.Data;
using StoreWebApi.MiddleWares;
using Microsoft.EntityFrameworkCore;
using StoreWeb.Repository.Identity.Contexts;
using StoreWeb.Repository.Identity.DataSeed;
using Microsoft.AspNetCore.Identity;
using StoreWeb.Core.Entities.Identity;

namespace StoreWebApi.Helper
{
    public static class ConfigureMiddleware
    {

        public static async Task<WebApplication> ConfigureMiddlewareAsync(this WebApplication app)
        {

            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<StoreDbContext>();
            var identityDbContext = services.GetRequiredService<StoreIdentityDbContext>();
            var userManager = services.GetRequiredService<UserManager<AppUser>>();
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                await context.Database.MigrateAsync();
                await StoreDbContextSeed.SeedAsync(context);
                await identityDbContext.Database.MigrateAsync();
                await StoreIdentityDbContextSeed.SeedAppUserAsync(userManager);

            }
            catch (Exception ex)
            {
                var loger = loggerFactory.CreateLogger<Program>();
                loger.LogError(ex.Message);
                loger.LogError(ex, "There are Problems during apply migrations");
            }

            //StoreDbContext context = new StoreDbContext();
            //context.Database.MigrateAsync(); // Update-DataBase

            app.UseMiddleware<ExceptionMiddleWare>(); // Configure User-Defined MiddleWare


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            

            app.UseStatusCodePagesWithReExecute("/error/{0}");

            app.MapControllers();


            return app;
        }


    }
}
