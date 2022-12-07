using API.Errors;
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
            /* AddScoped Creates an instance of the ProductRepository when API receives 
            an HTTP request and Create a new instance of the Controllers, disposes it later */
            services.AddScoped<IProductRepository, ProductRepository>();

            // Adding Generic Repository
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<ILoggerManager, LoggerManager>();

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