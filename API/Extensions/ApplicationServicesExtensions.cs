using API.Errors;
using API.Helpers;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Logg;
using Microsoft.AspNetCore.Mvc;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddSingleton<StoreContext>();
            /* AddScoped Creates an instance of the ProductRepository when API receives 
            an HTTP request and Create a new instance of the Controllers, disposes it later */
            services.AddScoped<IProductRepository, ProductRepository>();
            //Adding auto Mapper (Nuget Package for concatenating URLs received from API requests)
            services.AddAutoMapper(typeof(Mapper));  // Could slow your application 
            // Adding Generic Repository
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddEndpointsApiExplorer();

            services.AddScoped<ILoggerManager, LoggerManager>();

            //Adding CORS Policy

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");  //Client App Url
                });
            });
            services.Configure<ApiBehaviorOptions>(options => 
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                            .Where(e => e.Value.Errors.Count > 0)
                            .SelectMany(x => x.Value.Errors)
                            .Select(x => x.ErrorMessage).ToArray();
                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });
            return services;
        }
    }
}