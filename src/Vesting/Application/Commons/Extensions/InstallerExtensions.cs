using System.Reflection;
using Application.Commons.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Commons.Extensions;

public static class InstallerExtensions
{
    public static IServiceCollection InstallServices(this IServiceCollection services)
    {
        var installers = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => type.IsClass && typeof(IServiceInstaller).IsAssignableFrom(type));

        foreach (var installer in installers)
        {
            var serviceInstaller = (IServiceInstaller)Activator.CreateInstance(installer)!;
            serviceInstaller.InstallServices(services);
        }

        return services;
    }

    public static IServiceCollection AddSingletonWithValidation<TInterface, TUseCase>(
        this IServiceCollection services, Func<TUseCase, TInterface> getValidationInstance)
        where TInterface: class where TUseCase: class, TInterface 
        {
            services.AddSingleton<TUseCase>();
            services.AddSingleton<TInterface>(provider =>
            {
                var instance = provider.GetRequiredService<TUseCase>();
                return getValidationInstance(instance);
            });
            return services;
        }
}