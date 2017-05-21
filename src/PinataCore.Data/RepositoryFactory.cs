using MongoDB.Driver;

namespace PinataCore.Data
{
    public class RepositoryFactory
    {
        public static IPinataCoreRepository Create(string connectionString, Provider.Type type)
        {
            IPinataCoreRepository repository = null;

            switch (type)
            {
                case Provider.Type.MySQL:
                    {
                        repository = new MySQL.PinataCoreRepository(connectionString, Provider.MySQL);
                        break;
                    }
                case Provider.Type.MongoDB:
                    {
                        repository = new MongoDB.PinataCoreRepository(new MongoUrl(connectionString));
                        break;
                    }
            }

            return repository;
        }
    }
}