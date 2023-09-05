#region Using

using Softy.EasyServiceRegistration.Microsoft.DependencyInjection.Core.Attributes;

#endregion

namespace Tests.RegisterAs2Interfaces;

public interface IInterface1 { }

public interface IInterface2 { }

[RegisterService<IInterface1>]
[RegisterService<IInterface2>]
internal class InternalClassWith2Interfaces { }