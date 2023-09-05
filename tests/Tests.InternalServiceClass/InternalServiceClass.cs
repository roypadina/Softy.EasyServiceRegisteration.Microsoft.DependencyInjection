#region Using

using Softy.EasyServiceRegistration.Microsoft.DependencyInjection.Core.Attributes;

#endregion

namespace Tests.InternalServiceClass;

public interface IInternalServiceInterface { }

[RegisterService<IInternalServiceInterface>]
internal class InternalServiceClass : IInternalServiceInterface { }