using Grpc.Net.Client;
using GrpcStream;
using static System.Console;

WriteLine("Start Streaming gRPC");

var channel = GrpcChannel.ForAddress("https://localhost:7251");
var client = new StreamNumber.StreamNumberClient(channel);

using var call = client.SendNumber();
for (int i = 0; i < 20; i++)
{
    Write("Enter Number : ");
    var numberString = ReadLine();
    _ = int.TryParse(numberString, out int number);
    await call.RequestStream.WriteAsync(new() { Value = number });
    WriteLine($"Send To Server {number}");
}
await call.RequestStream.CompleteAsync();
var response = await call;
WriteLine($"Response : {response.Result}");