using CardStorageService.Data;
using CardStorageService.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace CardStorageService.Services.Impl
{
    public class CardRepository : ICardRepositoryService
    {
        #region Services

        private readonly CardStorageServiceDbContext _context;
        private readonly ILogger<ClientRepository> _logger;
        private readonly IOptions<DatabaseOptions> _databaseOptions;

        #endregion

        #region Constructors

        public CardRepository(
            ILogger<ClientRepository> logger,
            IOptions<DatabaseOptions> databaseOptions,
            CardStorageServiceDbContext context)
        {
            _logger = logger;
            _databaseOptions = databaseOptions;
            _context = context;
        }

        public string Create(Card data)
        {
            var client = _context.Clients.FirstOrDefault(client => client.ClientId == data.ClientId);
            if (client == null)
                throw new Exception("Client not found.");

            _context.Cards.Add(data);

            _context.SaveChanges();

            return data.CardId.ToString();
        }

        public IList<Card> GetByClientId(string id)
        {
            List<Card> cards = new List<Card>();
            using (SqlConnection sqlConnection = new SqlConnection(_databaseOptions.Value.ConnectionString))
            {
                sqlConnection.Open();
                using (var sqlCommand = new SqlCommand(String.Format("select * from cards where ClientId = {0}", id), sqlConnection))
                {
                    var reader = sqlCommand.ExecuteReader();
                    while (reader.Read())
                    {
                        cards.Add(new Card
                        {
                            CardId = new Guid(reader["CardId"].ToString()),
                            CardNo = reader["CardNo"]?.ToString(),
                            Name = reader["Name"]?.ToString(),
                            CVV2 = reader["CVV2"]?.ToString(),
                            ExpDate = Convert.ToDateTime(reader["ExpDate"])
                        });
                    }
                }

            }
            return cards;

            //return _context.Cards.Where(card => card.ClientId == id).ToList();
        }

        public int Delete(string id)
        {
            Card card = GetById(id);
            if (card == null)
                return 0;
            _context.Cards.Remove(card);
            _context.SaveChanges();
            return 1;
        }

        public IList<Card> GetAll()
        {
            return _context.Cards.ToList();
        }

        public int Update(Card data)
        {
            if (data == null) throw new ArgumentNullException("data is null");
            Card card = GetById(data.CardId);

            if (card != null)
            {
                card.CardId = data.CardId;
                card.CardNo = data.CardNo;
                card.Name = data.Name;
                card.CVV2 = data.CVV2;
                card.ExpDate = data.ExpDate;
                _context.SaveChanges();
                return 1;
            }

            return 0;
        }

        public Card GetById(string id)
        {
             Guid cardId = new Guid(id);
            return GetById(cardId);
        }

        #endregion

        #region Private Method
        private Card GetById(Guid id)
        {
            return _context.Cards.FirstOrDefault(card => card.CardId == id);
        }

        #endregion
    }
}
