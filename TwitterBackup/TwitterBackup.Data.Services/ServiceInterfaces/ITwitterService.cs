using System.Collections.Generic;
using TwitterBackup.Data.DTO;

namespace TwitterBackup.Data.Services.ServiceInterfaces
{
    public interface ITwitterService
    {
        ICollection<TweetDTO> GetTweets(string screenName);
    }
}
