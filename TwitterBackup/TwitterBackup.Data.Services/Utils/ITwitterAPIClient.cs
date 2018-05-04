namespace TwitterBackup.Data.Services.Utils
{
    public interface ITwitterAPIClient
    {
        string GetTweets(string screenName);
    }
}