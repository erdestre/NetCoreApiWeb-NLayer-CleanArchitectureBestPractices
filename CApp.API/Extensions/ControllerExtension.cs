using CApp.API.Filters;

namespace CApp.API.Extensions
{
    public static class ControllerExtension
    {
        public static IServiceCollection AddControllerWithFiltersExt(this IServiceCollection services)
        {
            services.AddScoped(typeof(NotFoundFilter<,>));
            services.AddControllers(options =>
            {
                options.Filters.Add<FluentValidationFilter>();
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            });
            return services;
        }
    }
}
