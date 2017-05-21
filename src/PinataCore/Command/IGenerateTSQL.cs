using PinataCore.Common;
using System.Collections.Generic;

namespace PinataCore.Command
{
    public interface IGenerateTSQL
    {
        void CreateTSQL(SampleSQLData sample, IList<object> sqlList);

        void CreateTSQL(SampleSQLData sample, IList<object> sqlList, IDictionary<string, string> dynamicParameters);
    }
}
