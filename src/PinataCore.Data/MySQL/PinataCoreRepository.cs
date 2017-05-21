using Dapper;
using SocialMiner.Core.Data.SQL;
using System;
using System.Collections.Generic;
using System.Data;

namespace PinataCore.Data.MySQL
{
    public class PinataCoreRepository : BaseRepository, IPinataCoreRepository
    {
        #region [ PRIVATE ]

        private bool ExecuteCommand(IList<object> list)
        {
            foreach (var sql in list)
            {
                using (IDbConnection connection = GetConnection())
                {
                    connection.Execute(sql.ToString(), null, null, 0, null);
                }
            }

            return true;
        }

        #endregion

        public PinataCoreRepository(string connectionString, string providerName)
            : base(connectionString, providerName)
        {
        }

        public PinataCoreRepository(IDbConnection connection)
            : base(connection)
        {
        }

        public bool Insert(IList<object> list)
        {
            return ExecuteCommand(list);
        }

        public bool Update(IList<object> list)
        {
            return ExecuteCommand(list);
        }

        public bool Delete(IList<object> list)
        {
            return ExecuteCommand(list);
        }
    }
}
