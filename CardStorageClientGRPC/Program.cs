using Grpc.Net.Client;
using System;
using static ClientServiceProtos.ClientService;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardStorageClientGRPC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Создать клиента...");
            Console.ReadLine();

            using (GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:5001"))
            {
                ClientServiceClient client = new ClientServiceClient(channel);
                var response = new client.CreateClientResponse(new ClientServiceProtos.CreateClientRequest
                {
                    FirstName = "Иван",
                    Surname = "Иванов",
                    Patronymic = "Иваныч",
                });
                Console.WriteLine($"ClientId: {response.ClientId}; ErrorCode: {response.ErrorCode}; ErrorMessage: {response.ErrorMessage}");
            }
        }
    }
}
