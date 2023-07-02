## Connect to Discount.Grpc

1. Click on the project and select Add -> Connected Service 
2. Select add gRPC connection and select the proto file and choose "Client" because this application will be the client who consume the DiscountService

This will add 
```csproj
<PackageReference Include="Grpc.AspNetCore" Version="2.49.0" />
```

and 

```csproj
<ItemGroup>
<Protobuf Include="..\..\Discount\Discount.Grpc\Protos\discount.proto" GrpcServices="Client">
    <Link>Protos\discount.proto</Link>
</Protobuf>
</ItemGroup>
```

Moreover, this will add folder "Protos" with "discount.proto" and this file should have Properties

a. "Build Action" = "Protobuf Compiler"
b. "gRPC Stub Classes" = "Client only"

3. Build app to auto generate code in a client mode (DiscountProtoServiceClient)

4. Create new class in folder "GrpcServices" for consuming gRPC services (that will inject gRPC client)

5. Register GrpcService in program.cs

```csharp
builder.Services.AddScoped<DiscountGrpcService>();
```

and 

```csharp
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
});
```
This address is the address that specifies how to reach the gRPC server. We need to provide the url of the app.
