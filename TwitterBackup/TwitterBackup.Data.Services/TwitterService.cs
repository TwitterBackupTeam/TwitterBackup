using System.Collections.Generic;
using System.Linq;
using TwitterBackup.Data.DTO;
using TwitterBackup.Data.Services.ServiceInterfaces;
using TwitterBackup.Data.Services.Utils;

namespace TwitterBackup.Data.Services
{
    public class TwitterService : ITwitterService
    {
        private readonly ITwitterAPIClient twitterApiClient;
        private readonly IJsonDeserializer jsonDeserializer;

        public TwitterService(ITwitterAPIClient twitterApiClient, IJsonDeserializer jsonDeserializer)
        {
            this.twitterApiClient = twitterApiClient;
            this.jsonDeserializer = jsonDeserializer;
        }

        public ICollection<TweetDTO> GetTweets(string screenName)
        {
            var json = this.twitterApiClient.GetTweets(screenName);

            var result = this.jsonDeserializer.Deserialize<TweetDTO[]>(json);

            return result.ToList();
        }
    }
}