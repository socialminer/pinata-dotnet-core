using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using SocialMiner.Core.Data;
using SocialMiner.Core;

namespace PinataCore.Data.MongoDB
{
    public class PinataCoreRepository : BaseMongoRepository<BsonDocument, Guid>, IPinataCoreRepository
    {
        #region [ CONSTRUCTORS ]

        public PinataCoreRepository(MongoUrl mongoUrl)
            : base(mongoUrl)
        {
        }

        #endregion

        public bool Insert(IList<object> list)
        {
            try
            {
                foreach (var item in list)
                {
                    var data = ((IDictionary<string, IList<BsonDocument>>)item).First();

                    CollectionName = data.Key;
                    CreateBatchAsync(data.Value).Wait();
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Update(IList<object> list)
        {
            throw new NotImplementedException();
        }

        public bool Delete(IList<object> list)
        {
            try
            {
                foreach (var element in list)
                {
                    var data = ((IDictionary<string, IList<BsonElement>>)element).First();

                    CollectionName = data.Key;

                    foreach(BsonElement item in data.Value)
                    {
                        // TODO: Implementar utilização de GUID legado (CSUUID)
                        BsonDocument query = CreateQuery("{'" + item.Name + "': @value }", new { value = item.Value });

                        DeleteAsync(query).Wait();
                    };
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}