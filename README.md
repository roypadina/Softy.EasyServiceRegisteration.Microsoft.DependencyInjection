# Softy.EasyServiceRegisteration.Microsoft.DependencyInjection [![NuGet](https://img.shields.io/nuget/v/Softy.EasyServiceRegistration.Microsoft.DependencyInjection.Core)](https://www.nuget.org/packages/Softy.EasyServiceRegistration.Microsoft.DependencyInjection.Core/)
Microsoft DI service registration made easy using attributes 



Tired of adding each new service to the builder ? stop dong that!
use easy-to-use attributes on services classes and let them be added automaticlly!

services can be registered as thier own implementaion, or with one or more interfaces/
services can be private nested classes , internal class or public classes
by default, services are registered with ```ServiceLifetime.Scoped```


How To Use:

decoarate your service class with the ```RegisterService``` attribute
```

[RegisterService]
public class PublicServiceClass { }

```

add EasyServices to the service collection builder:

```

services.AddEasyServices(<assemblyOfYourServices>);

```

That is it!, the serviec will be registered and you can use it ! (in this case, as its own implementaion, i.e ```PublicServiceClass```)


Register service without interface :

```

[RegisterService]
public class ServiceClass { }

```


Regsiter service with interface:

```
public interface IInternalServiceInterface { }

[RegisterService<IInternalServiceInterface>]
internal class InternalServiceClass : IInternalServiceInterface { }
```
or

```
public interface IInternalServiceInterface { }

[RegisterService(ImplementationType = typeof(IInternalServiceInterface))]
internal class InternalServiceClass : IInternalServiceInterface { }
```

register service with 2 (or more) interfaces (just use the same attribute more and more

```
public interface IInterface1 { }

public interface IInterface2 { }

[RegisterService<IInterface1>]
[RegisterService<IInterface2>]
internal class InternalClassWith2Interfaces { }

```


Ignoring types from builder:

```
services.AddEasyServices(x => x == typeof(ServiceClass), typeof(ServiceClass).Assembly);
 ```
Or
```
services.AddEasyServices(x => x.Name == "ServiceClass", typeof(ServiceClass).Assembly);
```
 any predication on the type will work!

 Changing  the service lifetime:

```
public interface IInternalServiceInterface { }

[RegisterService<IInternalServiceInterface>(ServiceLifetime = ServiceLifetime.Singleton)]
internal class InternalServiceClass : IInternalServiceInterface { }
```

 
 






