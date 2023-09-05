#region Using

using System;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace Softy.EasyServiceRegistration.Microsoft.DependencyInjection.Core.Attributes;

/// <summary>
///     Mark the decorated class as a service to be registered in the IServiceCollection
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterServiceAttribute : Attribute
{
    #region Public Members

    /// <summary>
    ///     The service lifetime to register as
    ///     default to Scoped
    /// </summary>
    public ServiceLifetime ServiceLifetime { get; set; } = ServiceLifetime.Scoped;

    /// <summary>
    /// </summary>
    public Type ImplementationType { get; set; }

    #endregion
}

/// <summary>
///     Mark the decorated class as a service to be registered in the IServiceCollection as the given interface in the
///     generic parameter
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterServiceAttribute<TInterface> : RegisterServiceAttribute
{
    #region Constructors

    public RegisterServiceAttribute()
    {
        ImplementationType = typeof(TInterface);
    }

    #endregion
}