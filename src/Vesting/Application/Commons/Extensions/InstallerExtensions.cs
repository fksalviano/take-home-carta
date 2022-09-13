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
        var installersTypes = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => typeof(IServiceInstaller).IsAssignableFrom(type) && type.IsClass);

        foreach (var type in installersTypes)
        {
            var installer = (IServiceInstaller)Activator.CreateInstance(type)!;
            installer.InstallServices(services);
        }
        return services;
    }

    public static T GetService<T>(this IServiceCollection services) =>
        services.BuildServiceProvider().GetService<T>()!;

    public static IServiceCollection AddSingletonWithValidation<TInterface, TUseCase>(
        this IServiceCollection services, Func<TUseCase, TInterface> getValidation)
        where TInterface: class where TUseCase: class, TInterface =>
            services
                .AddSingleton<TUseCase>()
                .AddSingleton<TInterface>(provider =>
                    getValidation(provider.GetRequiredService<TUseCase>()));

    public static IServiceCollection AddSingletonWithValidation<TInterface, TUseCase>(
        this IServiceCollection services, Func<TUseCase, IServiceProvider, TInterface> getValidation)
        where TInterface: class where TUseCase: class, TInterface =>
            services
                .AddSingleton<TUseCase>()
                .AddSingleton<TInterface>(provider =>
                    getValidation(provider.GetRequiredService<TUseCase>(), provider));
}