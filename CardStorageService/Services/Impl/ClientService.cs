using ClientServiceProtos;
using CardStorageService.Data;
using Grpc.Core;
using static ClientServiceProtos.ClientService;

namespace CardStorageService.Services.Impl
{
    public class ClientService : ClientServiceBase
    {
        private readonly IClientRepositoryService _clientRepositoryService;
        private readonly ILogger<ClientRepository> _logger;

        public ClientService (IClientRepositoryService clientRepositoryService, ILogger<ClientRepository> logger)
        {
            _clientRepositoryService = clientRepositoryService;
            _logger = logger;
        }

    

        #region gRPC

        public override Task<CreateClientResponse> Create(CreateClientRequest request, ServerCallContext context)
        {
            try
            {
                var clientId = _clientRepositoryService.Create(new Client 
                {
                    FirstName= request.FirstName,
                    Surname= request.Surname,
                    Patronymic = request.Patronymic,
                });
                var response = new CreateClientResponse
                {
                    ClientId = clientId,
                    ErrorCode = 0,
                    ErrorMessage = string.Empty
                };
                return Task.FromResult(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error create client");
                var response = new CreateClientResponse
                {
                    ClientId = -1,
                    ErrorCode = 912,
                    ErrorMessage = "Error create client"
                };
                return Task.FromResult(response);
            }
        }

        #endregion
    }
}
