using Newtonsoft.Json;

namespace TwitterBackup.Data.Services.Utils
{
    public class JsonDeserializer : IJsonDeserializer
    {
        public T Deserialize<T>(string str)
        {
            T objects = JsonConvert.DeserializeObject<T>(str);

            return objects;
        }
    }
}
