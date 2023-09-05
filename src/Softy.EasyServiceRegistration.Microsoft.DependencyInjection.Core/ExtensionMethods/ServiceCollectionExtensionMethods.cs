#region Using

using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Softy.EasyServiceRegistration.Microsoft.DependencyInjection.Core.Attributes;

#endregion

// ReSharper disable once CheckNamespace
namespace Softy.EasyServiceRegistration.Microsoft.DependencyInjection.Core.ExtensionMethods;

/// <summary>
///     Extenstion methods for the IServiceCollection to trigger the easy services registration
/// </summary>
public static class ServiceCollectionExtensionMethods
{
    #region Public Methods

    /// <summary>
    ///     Register all service decorated with the <see cref="RegisterServiceAttribute" /> in the calling assembly
    /// </summary>
    /// <param name="services">The ServiceCollection</param>
    /// <returns>The IServiceCollection</returns>
    public static IServiceCollection AddEasyServices(this IServiceCollection services)
        => services.AddEasyServices((Func<Type, bool>)null);

    /// <summary>
    ///     Register all service decorated with the <see cref="RegisterServiceAttribute" /> in the calling assembly
    /// </summary>
    /// <param name="services">The ServiceCollection</param>
    /// <param name="typesToIgnorePredicate">
    ///     predicate to ignore types, so types that match the given predicate will be ignored
    ///     and will not be registered
    /// </param>
    /// <returns>The IServiceCollection</returns>
    public static IServiceCollection AddEasyServices(this IServiceCollection services,
        Func<Type, bool> typesToIgnorePredicate)
        => services.AddEasyServices(typesToIgnorePredicate, Assembly.GetCallingAssembly());

    /// <summary>
    ///     Register all service decorated with the <see cref="RegisterServiceAttribute" /> in the given assemblies
    /// </summary>
    /// <param name="services">The ServiceCollection</param>
    /// <param name="assemblies">The assemblies to scan</param>
    /// <returns></returns>
    public static IServiceCollection AddEasyServices(this IServiceCollection services, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
            _ = services.AddEasyService(null, assembly);
        return services;
    }

    /// <summary>
    ///     Register all service decorated with the <see cref="RegisterServiceAttribute" /> in the given assemblies
    /// </summary>
    /// <param name="services">The ServiceCollection</param>
    /// <param name="typesToIgnorePredicate">
    ///     predicate to ignore types, so types that match the given predicate will be ignored
    ///     and will not be registered
    /// </param>
    /// <param name="assemblies">The assemblies to scan</param>
    /// <returns></returns>
    public static IServiceCollection AddEasyServices(this IServiceCollection services,
        Func<Type, bool> typesToIgnorePredicate, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
            _ = services.AddEasyService(typesToIgnorePredicate, assembly);
        return services;
    }

    /// <summary>
    ///     Register all service decorated with the <see cref="RegisterServiceAttribute" /> in the given assembly
    /// </summary>
    /// <param name="services">The ServiceCollection</param>
    /// <param name="typesToIgnorePredicate">
    ///     predicate to ignore types, so types that match the given predicate will be ignored
    ///     and will not be registered
    /// </param>
    /// <param name="assembly">The Assembly to scan</param>
    /// <returns></returns>
    private static IServiceCollection AddEasyService(this IServiceCollection services,
        Func<Type, bool> typesToIgnorePredicate, Assembly assembly)
    {
        var types = assembly.GetTypes();
        foreach (var type in types.Where(x => typesToIgnorePredicate == null || !typesToIgnorePredicate.Invoke(x)))
        foreach (var registerServiceAttribute in type.GetCustomAttributes<RegisterServiceAttribute>())
            services.Add(new(registerServiceAttribute.ImplementationType ?? type, type,
                registerServiceAttribute.ServiceLifetime));
        return services;
    }

    #endregion
}