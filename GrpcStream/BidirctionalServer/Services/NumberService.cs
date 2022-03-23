using Grpc.Core;
using GrpcStream;

namespace BidirctionalServer.Services;

public class NumberService : StreamNumber.StreamNumberBase
{
    public override async Task SendNumber(IAsyncStreamReader<SendNumberRequest> requestStream, IServerStreamWriter<NumberReply> responseStream, ServerCallContext context)
    {
        await foreach (var item in requestStream.ReadAllAsync())
        {
            await responseStream.WriteAsync(new()
            {
                Index = item.Index,
                Result = item.Value * 2
            });
        }
    }
}