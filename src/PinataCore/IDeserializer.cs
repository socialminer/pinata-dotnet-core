using System.Collections.Generic;

namespace PinataCore
{
    public interface IDeserializer
    {
        IList<object> DeserializeData(string samplePath);
    }
}