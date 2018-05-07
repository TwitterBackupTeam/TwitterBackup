using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterBackup.Data.DTO;
using TwitterBackup.Data.Services.ServiceInterfaces;
using TwitterBackup.Data.Services.Utils;

namespace TwitterBackup.Data.Services
{
    public class TwitterApiService : ITwitterAPIService
    {
        private readonly ITwitterAPIClient twitterApiClient;
        private readonly IJsonDeserializer jsonDeserializer;

        public TwitterApiService(ITwitterAPIClient twitterApiClient, IJsonDeserializer jsonDeserializer)
        {
            this.twitterApiClient = twitterApiClient;
            this.jsonDeserializer = jsonDeserializer;
        }

        public async Task<ICollection<TweetDTO>> GetTweets(string screenName)
        {
            var json = await this.twitterApiClient.GetTweets(screenName);
            
            var result = this.jsonDeserializer.Deserialize<TweetDTO[]>(json);

            foreach (var tweetDto in result)
            {
                if (tweetDto.Text.LastIndexOf(@"https://") > 0)
                {
                    //remove link @ the end
                    tweetDto.Text = tweetDto.Text.Substring(0, tweetDto.Text.LastIndexOf(@"https://") - 1);
                }
            }

            return result.ToList();
        }

		public async Task<TweeterDTO[]> GetTweetersByScreenName(string screenName)
		{
			if (string.IsNullOrEmpty(screenName))
			{
				throw new ArgumentNullException(nameof(screenName));
			}

			var searchString = "https://api.twitter.com/1.1/users/search.json?q=";
			var foundTweetersString = await this.twitterApiClient.GetTwitterJsonData(searchString + screenName.Trim());
			var deserializedTweeter = this.jsonDeserializer.Deserialize<TweeterDTO[]>(foundTweetersString);
			return deserializedTweeter;
		}

		public async Task<TweeterDTO> GetTweeterInfoById(long id)
		{

			var searchString = "https://api.twitter.com/1.1/users/show.json?user_id=";
			var foundTweetersString = await this.twitterApiClient.GetTwitterJsonData(searchString + id.ToString());
			var deserializedTweeter = this.jsonDeserializer.Deserialize<TweeterDTO>(foundTweetersString);
			return deserializedTweeter;
		}
	}
}