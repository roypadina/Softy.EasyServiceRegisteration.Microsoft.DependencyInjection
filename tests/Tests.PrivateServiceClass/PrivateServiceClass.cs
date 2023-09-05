#region Using

using Softy.EasyServiceRegistration.Microsoft.DependencyInjection.Core.Attributes;

#endregion

namespace Tests.PrivateServiceClass;

public interface IPrivateServiceInterface { }

internal class InternalClass
{
    #region Nexted Types

    [RegisterService<IPrivateServiceInterface>]
    private class PrivateServiceClass : IPrivateServiceInterface { }

    #endregion
}