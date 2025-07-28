using Rento.Api.Mapping;

namespace Rento.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddMappings();

            return services;
        }
    }
}
