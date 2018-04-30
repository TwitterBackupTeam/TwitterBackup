namespace TwitterBackup.Data.Services.Utils
{
    public interface ITwitterClient
    {
        string GetTweets(string screenName);
    }
}
