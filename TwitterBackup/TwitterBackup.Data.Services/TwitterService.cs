using System.Collections.Generic;
using TwitterBackup.Data.DTO;
using TwitterBackup.Data.Services.Utils;

namespace TwitterBackup.Data.Services
{
    public class TwitterService
    {
        private readonly ITwitterClient twitterClient;

        public TwitterService(ITwitterClient twitterClient)
        {
            this.twitterClient = twitterClient;
        }

        public ICollection<TweetDTO> GetTweets(string screenName)
        {
            var list = new List<TweetDTO>();

            var res = this.twitterClient.GetTweets(screenName);


            return list;
        }
    }
}