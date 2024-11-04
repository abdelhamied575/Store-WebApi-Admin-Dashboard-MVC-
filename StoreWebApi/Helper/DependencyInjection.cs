using Microsoft.EntityFrameworkCore;
using StoreWeb.Core.Services.Contract;
using StoreWeb.Core;
using StoreWeb.Repository;
using StoreWeb.Repository.Data.Contexts;
using StoreWeb.Services.Services.Products;
using StoreWeb.Core.Mapping.Products;
using Microsoft.AspNetCore.Mvc;
using StoreWebApi.Errors;
using StoreWeb.Core.Repositories.Contract;
using StoreWeb.Repository.Repositories;
using StackExchange.Redis;
using StoreWeb.Core.Mapping.Basket;
using StoreWeb.Services.Services.Cashes;
using StoreWeb.Repository.Identity.Contexts;
using StoreWeb.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using StoreWeb.Services.Services.Tokens;
using StoreWeb.Services.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using StoreWeb.Core.Mapping.Auth;
using StoreWeb.Core.Mapping.Orders;
using StoreWeb.Services.Services.Orders;
using StoreWeb.Services.Services.Baskets;
using StoreWeb.Services.Services.Payments;

namespace StoreWebApi.Helper
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddDependency(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddBuiltInService();
            services.AddBuiltInService();
            services.AddSwiggerService();
            services.AddDbContextService(configuration);
            services.AddUserDefinendService();
            services.AddAutoMapperService(configuration);
            services.ConfigureInValidModelStateResponseService(configuration);
            services.AddRedisService(configuration);
            services.AddIdentityService();
            services.AddAuthenticationService(configuration);

            return services;

        }

        private static IServiceCollection AddBuiltInService(this IServiceCollection services)
        {
            services.AddControllers();

            return services;

        }


        private static IServiceCollection AddSwiggerService(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;

        }

        private static IServiceCollection AddDbContextService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddDbContext<StoreIdentityDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });


            return services;

        }

        private static IServiceCollection AddUserDefinendService(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICashService,CasheService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IPaymentService, PaymentService>();
            return services;

        }

        private static IServiceCollection AddAutoMapperService(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddAutoMapper(M => M.AddProfile(new ProductProfile(configuration)));
            services.AddAutoMapper(M => M.AddProfile(new BasketProfile()));
            services.AddAutoMapper(M => M.AddProfile(new AuthProfile()));
            services.AddAutoMapper(M => M.AddProfile(new OrderProfile(configuration)));

            return services;

        }
        
        private static IServiceCollection ConfigureInValidModelStateResponseService(this IServiceCollection services,IConfiguration configuration)
        {


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (ActionContext =>
                {
                    var errors = ActionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                            .SelectMany(P => P.Value.Errors)
                                            .Select(E => E.ErrorMessage)
                                            .ToArray();

                    var response = new ApiValidationErrorResponse()
                    {

                        Errors = errors
                    };

                    return new BadRequestObjectResult(response);
                    //return BadRequest(response); // Error due to Not Inhert From ApiError Class

                });
            });


            return services;

        }

        private static IServiceCollection AddRedisService(this IServiceCollection services,IConfiguration configuration)
        {


            services.AddSingleton<IConnectionMultiplexer>((serviceProvider) =>
            {
                var connection = configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(connection);
            });

            return services;

        }

        private static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            services.AddIdentity<AppUser,IdentityRole>()
                    .AddEntityFrameworkStores<StoreIdentityDbContext>();

            return services;

        }
        private static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });

            return services;

        }



    }
}
