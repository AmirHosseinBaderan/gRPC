using Grpc.Core;

namespace GrpcStreamServer.Services;

public class NumberStreamService : StreamNumber.StreamNumberBase
{
    public override async Task SendNumber(SendNumberRequest request, IServerStreamWriter<NumberReply> responseStream, ServerCallContext context)
    {
        Random random = new();
        for (int i = 0; i < request.Value; i++)
        {
            var rnd = random.Next(0, i);
            await responseStream.WriteAsync(new() { Result = rnd });
        }
    }
}