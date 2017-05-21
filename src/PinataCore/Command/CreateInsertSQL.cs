using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PinataCore.Common;
using SocialMiner.Core;
using System;
using System.Collections.Generic;

namespace PinataCore.Command
{
    public class CreateInsertSQL : IGenerateTSQL
    {
        public void CreateTSQL(SampleSQLData sample, IList<object> sqlList)
        {
            string baseSQL = @"INSERT INTO {0} ({1}) VALUES ({2});";
            string dataSQL = string.Empty;

            foreach (var row in sample.Rows)
            {
                dynamic dynObj = JsonConvert.DeserializeObject(row.ToString());

                JObject jObj = (JObject)dynObj;

                JToken jToken;

                string value = string.Empty;

                string fields = string.Empty;
                string values = string.Empty;

                foreach (var schema in sample.Schema)
                {
                    fields += "`{0}`,".FormatWith(schema.Column);

                    jObj.TryGetValue(schema.Column, out jToken);

                    value = jToken.ToString();

                    values += "{0},".FormatWith(ParserDataType.ParseSQL((ParserDataType.DataType)Enum.Parse(typeof(ParserDataType.DataType), schema.Type, true), value));
                }

                dataSQL += baseSQL.FormatWith(sample.Table, fields.Substring(0, fields.LastIndexOf(',')), values.Substring(0, values.LastIndexOf(',')));
            }

            if (!string.IsNullOrEmpty(dataSQL))
            {
                sqlList.Add(dataSQL);
            }
        }

        public void CreateTSQL(SampleSQLData sample, IList<object> sqlList, IDictionary<string, string> parameters)
        {
            string baseSQL = @"INSERT INTO {0} ({1}) VALUES ({2});";
            string dataSQL = string.Empty;

            foreach (var row in sample.Rows)
            {
                dynamic dynObj = JsonConvert.DeserializeObject(row.ToString());

                JObject jObj = (JObject)dynObj;

                JToken jToken;

                string value = string.Empty;

                string fields = string.Empty;
                string values = string.Empty;

                foreach (var schema in sample.Schema)
                {
                    fields += "{0},".FormatWith(schema.Column);

                    jObj.TryGetValue(schema.Column, out jToken);

                    value = jToken.ToString();

                    if (parameters.ContainsKey(value))
                    {
                        value = parameters[value];
                    }

                    values += "{0},".FormatWith(ParserDataType.ParseSQL((ParserDataType.DataType)Enum.Parse(typeof(ParserDataType.DataType), schema.Type, true), value));
                }

                dataSQL += baseSQL.FormatWith(sample.Table, fields.Substring(0, fields.LastIndexOf(',')), values.Substring(0, values.LastIndexOf(',')));
            }

            if (!string.IsNullOrEmpty(dataSQL))
            {
                sqlList.Add(dataSQL);
            }
        }
    }
}