using Grpc.Core;

namespace GrpcStream.Services;

public class NumberStreamService : StreamNumber.StreamNumberBase
{
    public override async Task<NumberReply> SendNumber(IAsyncStreamReader<SendNumberRequest> requestStream, ServerCallContext context)
    {
        int result = 0;
        await foreach (var item in requestStream.ReadAllAsync())
        {
            result += item.Value;
        }

        return new NumberReply { Result = result };
    }
}