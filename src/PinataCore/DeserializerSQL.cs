using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using PinataCore.Common;

namespace PinataCore
{
    public class DeserializerSQL : IDeserializer
    {
        public IList<object> DeserializeData(string samplePath)
        {
            List<object> sampleDataList = new List<object>();

            string sampleRaw = File.ReadAllText(Path.GetFullPath(samplePath));

            sampleDataList.AddRange(JsonConvert.DeserializeObject<IList<SampleSQLData>>(sampleRaw));

            return sampleDataList;
        }
    }
}