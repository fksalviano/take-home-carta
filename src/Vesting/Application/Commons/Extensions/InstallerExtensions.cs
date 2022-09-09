using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Application.Commons.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Commons.Extensions;

[ExcludeFromCodeCoverage]
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
        return services
            .AddSingleton<TUseCase>()
            .AddSingleton<TInterface>(provider =>
            {
                var instance = provider.GetRequiredService<TUseCase>();
                return getValidationInstance(instance);
            });
    }

    public static T GetService<T>(this IServiceCollection services) =>
        services.BuildServiceProvider().GetService<T>()!;
}