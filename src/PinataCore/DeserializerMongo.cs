using System.Collections.Generic;
using System.IO;
using PinataCore.Common;
using Newtonsoft.Json;

namespace PinataCore
{
    public class DeserializerMongo : IDeserializer
    {
        public IList<object> DeserializeData(string samplePath)
        {
            List<object> sampleDataList = new List<object>();

            string sampleRaw = File.ReadAllText(Path.GetFullPath(samplePath));

            sampleDataList.AddRange(JsonConvert.DeserializeObject<IList<SampleMongoData>>(sampleRaw));

            return sampleDataList;
        }
    }
}
