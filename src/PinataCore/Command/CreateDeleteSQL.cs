using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PinataCore.Common;
using SocialMiner.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PinataCore.Command
{
    public class CreateDeleteSQL : IGenerateTSQL
    {
        public void CreateTSQL(SampleSQLData sample, IList<object> sqlList)
        {
            string baseSQL = @"DELETE FROM {0} WHERE {1};";
            string dataSQL = string.Empty;

            foreach (var row in sample.Rows)
            {
                dynamic dynObj = JsonConvert.DeserializeObject(row.ToString());

                JObject jObj = (JObject)dynObj;
                
                string value = string.Empty;
                string fields = string.Empty;

                foreach (var key in sample.Keys)
                {
                    Schema schema = sample.Schema.SingleOrDefault(s => s.Column == key);

                    if (!jObj.TryGetValue(schema.Column, out JToken jToken))
                        throw new ArgumentException($"Column '{schema.Column}' not found on data rows provided.");

                    value = jToken.ToString();

                    string parsedValue = "{0}".FormatWith(ParserDataType.ParseSQL((ParserDataType.DataType)Enum.Parse(typeof(ParserDataType.DataType), schema.Type, true), value));

                    fields += "{0}={1} AND ".FormatWith(schema.Column, parsedValue);
                }

                dataSQL += baseSQL.FormatWith(sample.Table, fields.Substring(0, fields.LastIndexOf("AND")));
            }

            if (!string.IsNullOrEmpty(dataSQL))
            {
                sqlList.Add(dataSQL);
            }
        }

        public void CreateTSQL(SampleSQLData sample, IList<object> sqlList, IDictionary<string, string> parameters)
        {
            string baseSQL = @"DELETE FROM {0} WHERE {1};";
            string dataSQL = string.Empty;

            foreach (var row in sample.Rows)
            {
                dynamic dynObj = JsonConvert.DeserializeObject(row.ToString());

                JObject jObj = (JObject)dynObj;
                string fields = string.Empty;

                foreach (var key in sample.Keys)
                {
                    Schema schema = sample.Schema.SingleOrDefault(s => s.Column == key);

                    jObj.TryGetValue(schema.Column, out JToken jToken);

                    string value = jToken.ToString();

                    if (parameters.ContainsKey(value))
                    {
                        value = parameters[value];
                    }

                    string parsedValue = "{0}".FormatWith(ParserDataType.ParseSQL((ParserDataType.DataType)Enum.Parse(typeof(ParserDataType.DataType), schema.Type, true), value));

                    fields += "{0}={1} AND ".FormatWith(schema.Column, parsedValue);
                }

                dataSQL += baseSQL.FormatWith(sample.Table, fields.Substring(0, fields.LastIndexOf("AND")));
            }

            if (!string.IsNullOrEmpty(dataSQL))
            {
                sqlList.Add(dataSQL);
            }
        }
    }
}