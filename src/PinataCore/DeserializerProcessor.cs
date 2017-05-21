using System.Collections.Generic;

namespace PinataCore
{
    public class DeserializerProcessor
    {
        public static IList<object> Execute(IDeserializer deserializer, string samplePath)
        {
            return deserializer.DeserializeData(samplePath);
        }
    }
}
