namespace TwitterBackup.Data.Services.Utils
{
    public interface IJsonDeserializer
    {
        T Deserialize<T>(string str);
    }
}
