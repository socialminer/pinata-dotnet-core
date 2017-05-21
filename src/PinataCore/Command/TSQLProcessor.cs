using PinataCore.Common;
using System.Collections.Generic;

namespace PinataCore.Command
{
    public class TSQLProcessor
    {
        public static void Execute(IGenerateTSQL sqlCommand, SampleSQLData sample, IList<object> sqlList)
        {
            sqlCommand.CreateTSQL(sample, sqlList);
        }

        public static void Execute(IGenerateTSQL sqlCommand, SampleSQLData sample, IList<object> sqlList, IDictionary<string, string> dynamicParameters)
        {
            sqlCommand.CreateTSQL(sample, sqlList, dynamicParameters);
        }
    }
}