#region Using

using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Softy.EasyServiceRegistration.Microsoft.DependencyInjection.Core.ExtensionMethods;
using Tests.InternalServiceClass;
using Tests.PrivateServiceClass;
using Tests.PublicServiceClass;
using Tests.RegisterAs2Interfaces;

#endregion

namespace Softy.EasyServiceRegistration.Microsoft.DependencyInjection.Tests;

public class RegisterTests
{
    #region Public Methods

    [Fact]
    public void PublicService_RegisterWithoutInterface()
    {
        // Arrange
        var sut = new ServiceCollection() as IServiceCollection;

        // Act
        _ = sut.AddEasyServices(typeof(PublicServiceClass).Assembly);

        // Assert
        _ = sut.Should().Contain(x =>
            x.ServiceType == typeof(PublicServiceClass) && x.ImplementationType == typeof(PublicServiceClass));
        _ = sut.Count(x => x.ServiceType == typeof(PublicServiceClass)).Should().Be(1);
        _ = sut.Count(x => x.ImplementationType == typeof(PublicServiceClass)).Should().Be(1);
    }

    [Fact]
    public void InternalService_RegisterWithPublicInterface()
    {
        // Arrange
        var sut = new ServiceCollection() as IServiceCollection;

        // Act
        _ = sut.AddEasyServices(typeof(IInternalServiceInterface).Assembly);

        // Assert
        _ = sut.Should().Contain(x =>
            x.ServiceType == typeof(IInternalServiceInterface) && x.ImplementationType!.Name == "InternalServiceClass");
        _ = sut.Count(x => x.ServiceType == typeof(IInternalServiceInterface)).Should().Be(1);
    }

    [Fact]
    public void PrivateNestedService_RegisterWithPublicInterface()
    {
        // Arrange
        var sut = new ServiceCollection() as IServiceCollection;

        // Act
        _ = sut.AddEasyServices(typeof(IPrivateServiceInterface).Assembly);

        // Assert
        _ = sut.Should().Contain(x =>
            x.ServiceType == typeof(IPrivateServiceInterface) && x.ImplementationType!.Name == "PrivateServiceClass");
        _ = sut.Count(x => x.ServiceType == typeof(IPrivateServiceInterface)).Should().Be(1);
    }

    [Fact]
    public void InternalClass_RegisterWith2Interfaces()
    {
        // Arrange
        var sut = new ServiceCollection() as IServiceCollection;

        // Act
        _ = sut.AddEasyServices(typeof(IInterface1).Assembly);

        // Assert
        _ = sut.Should().Contain(x =>
            x.ServiceType == typeof(IInterface1) && x.ImplementationType!.Name == "InternalClassWith2Interfaces");
        _ = sut.Count(x => x.ServiceType == typeof(IInterface1)).Should().Be(1);

        _ = sut.Should().Contain(x =>
            x.ServiceType == typeof(IInterface2) && x.ImplementationType!.Name == "InternalClassWith2Interfaces");
        _ = sut.Count(x => x.ServiceType == typeof(IInterface2)).Should().Be(1);
    }

    [Fact]
    public void IgnoreTypesByName()
    {
        // Arrange
        var sut = new ServiceCollection() as IServiceCollection;

        // Act
        _ = sut.AddEasyServices(x => x.Name == "InternalClassWith2Interfaces", typeof(IInterface1).Assembly);

        // Assert
        _ = sut.Should().BeEmpty();
    }

    [Fact]
    public void IgnoreTypesByType()
    {
        // Arrange
        var sut = new ServiceCollection() as IServiceCollection;

        // Act
        _ = sut.AddEasyServices(x => x == typeof(PublicServiceClass), typeof(PublicServiceClass).Assembly);

        // Assert
        _ = sut.Should().BeEmpty();
    }

    [Fact]
    public void LoadFrom2Assemblies()
    {
        // Arrange
        var sut = new ServiceCollection() as IServiceCollection;

        // Act
        _ = sut.AddEasyServices(typeof(PublicServiceClass).Assembly, typeof(IPrivateServiceInterface).Assembly);

        // Assert
        _ = sut.Should().HaveCount(2);
    }

    #endregion
}