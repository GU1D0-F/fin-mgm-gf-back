using FinManagerGf.Application;
using Microsoft.Extensions.DependencyInjection;

namespace FinManagerGf.IoC
{
    public static class AddApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(ApplicationRef).Assembly));

            return services;

        }
    }
}
