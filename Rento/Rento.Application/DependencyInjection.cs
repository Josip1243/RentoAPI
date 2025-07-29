using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Rento.Application.Authentication.Commands.Register;
using Rento.Application.Common.Behaviors;

namespace Rento.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;

            services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));
            services.AddValidatorsFromAssembly(assembly);

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddScoped<IValidator<RegisterCommand>, RegisterCommandValidator>();

            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(assembly);
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

            return services;
        }
    }
}
