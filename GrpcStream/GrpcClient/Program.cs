using Grpc.Core;
using Grpc.Net.Client;
using GrpcStreamServer;
using static System.Console;


WriteLine("Stream Data From Server To Client!");

var channel = GrpcChannel.ForAddress("https://localhost:7234");
var client = new StreamNumber.StreamNumberClient(channel);

Write("How Many Stream From Server : ");
string? numberStr = ReadLine();
_ = int.TryParse(numberStr, out int number);

using var request = client.SendNumber(new() { Value = number });
await foreach (var item in request.ResponseStream.ReadAllAsync())
{
    WriteLine($"Server Send : {item?.Result}");
}

WriteLine("Call Finished");