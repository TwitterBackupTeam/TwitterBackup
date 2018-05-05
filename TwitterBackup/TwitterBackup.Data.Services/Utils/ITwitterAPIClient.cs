using System.Threading.Tasks;

namespace TwitterBackup.Data.Services.Utils
{
    public interface ITwitterAPIClient
    {
        Task<string> GetTweets(string screenName);
    }
}