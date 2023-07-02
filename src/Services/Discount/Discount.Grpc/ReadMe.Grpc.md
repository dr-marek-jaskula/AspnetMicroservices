## Grpc

This is the best choice for synchronous communication of microservices.

Proto files defines interfaces that microservices in communication must implement, even if they are written in different languages.

For c# the required package is "Grpc.AspNetCore"

Important configuration in .cproj file is 

```cproj
<Protobuf Include="Protos\greet.proto" GrpcServices="Server" />
```

We can change the GrpcServices for instance to "Client" service if it is client service.

## Code generation from proto files

If we build the application, the Protobuf compiler will generate new classes in:

> obj/Debug/net7.0/Protos
