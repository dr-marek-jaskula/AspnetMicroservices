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

## Grpc services

Grpc Services and Proto files are like controllers for standard API approach.

1. At First create a new .proto file (discount.proto)

Click "properties" and set the "Build Action" to "Protobuf compiler", and gRPC Stub Classes to "Server Only"
Be sure that there is in project file 

```cproj
  <ItemGroup>
    <Protobuf Include="Protos\discount.proto" GrpcServices="Server" />
  </ItemGroup>
```

The version of syntax we use (use latest if possible)
```proto
syntax = "proto3";
```

The csharp namespace in which the generated code will be stored
```proto
option csharp_namespace = "Discount.Grpc.Protos";
```

Define the Service (that is like a controller) named "DiscountProtoService" that has a 
"GetDiscount" method that has input of GetDiscountRequest and returns CouponModel
```proto
service DiscountProtoService {
	rpc GetDiscount (GetDiscountRequest) returns (CouponModel);
}
```

Define complex types (request and responses)
```proto
message GetDiscountRequest {
	string productName = 1;
}
```
Very important thing is that we need to define field in an order that is defined by numbers.
So this = 1 is **not** a default value but a order definition (it says that this is the first field)

Other example
```proto
message CouponModel {
	int32 id = 1;
	string productName = 2;
	int32 description = 3;
	int32 amount = 4;
}
```

2. Build the application to make code generation

Examine if the new classes are created

3. Create Service classes
	a. Create DiscountService and inherit from DiscountProtoService.DiscountProtoServiceBase (generated one)
	b. write "override" and see what we can override.

4. Configure services

```csharp
app.MapGrpcService<DiscountService>();
```
