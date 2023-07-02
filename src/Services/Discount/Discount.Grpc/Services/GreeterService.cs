using Discount.Grpc;
using Grpc.Core;

namespace Discount.Grpc.Services;
public class GreeterService : Greeter.GreeterBase //this .GreeterBase is a generated class from Protobuf compiler based on a greet.proto contract
{
    private readonly ILogger<GreeterService> _logger;

    public GreeterService(ILogger<GreeterService> logger)
    {
        _logger = logger;
    }

    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        return Task.FromResult(new HelloReply
        {
            Message = "Hello " + request.Name
        });
    }
}
