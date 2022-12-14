using CardStorageService.Data;

namespace CardStorageService.Services.Impl
{
    public class ClientRepository : IClientRepositoryService
    {
        #region Services

        private readonly CardStorageServiceDbContext _context;
        private readonly ILogger<ClientRepository> _logger;

        #endregion

        #region Constructors

        public ClientRepository(
            ILogger<ClientRepository> logger,
            CardStorageServiceDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public int Create(Client data)
        {
            _context.Clients.Add(data);
            _context.SaveChanges();
            return data.ClientId;
        }

        public int Delete(int id)
        {
            Client client = GetById(id);
            if (client == null)
                return 0;
            _context.Clients.Remove(client);
            _context.SaveChanges();
            return 1;
        }

        public IList<Client> GetAll()
        {
            return _context.Clients.ToList();
        }

        public Client GetById(int id)
        {
            return _context.Clients.Where(x => x.ClientId == id).FirstOrDefault();
        }

        public int Update(Client data)
        {
            if (data == null) throw new ArgumentNullException("data is null");
            Client client = GetById(data.ClientId);

            if (client != null)
            {
                client.ClientId = data.ClientId;
                client.Cards = data.Cards;
                client.FirstName = data.FirstName;
                client.Surname= data.Surname;
                client.Patronymic= data.Patronymic;
                _context.SaveChanges();
                return 1;
            }

            return 0;
        }

        #endregion
    }
}
