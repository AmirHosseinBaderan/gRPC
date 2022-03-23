using Grpc.Core;
using Grpc.Net.Client;
using GrpcStream;
using static System.Console;


WriteLine("Bidirctional Streaming gRPC!");

var channel = GrpcChannel.ForAddress("https://localhost:7119");
var client = new StreamNumber.StreamNumberClient(channel);

Write("How Match Send Request  : ");
var numberStr = ReadLine();
_ = int.TryParse(numberStr, out int number);

using var callStream = client.SendNumber();
var call = Task.Run(async () =>
{
    Random random = new();
    for (int i = 0; i < number; i++)
    {
        var rnd = random.Next(0, i);
        await callStream.RequestStream.WriteAsync(new() { Index = i, Value = rnd });
        WriteLine($"Send To Server [{i}] : '{rnd}'");
    }
});

var result = Task.Run(async () =>
{
    await foreach (var item in callStream.ResponseStream.ReadAllAsync())
    {
        WriteLine($"Server Send Response [{item.Index}] : '{item.Result}'");
    }
});

await Task.WhenAll(call, result);