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
    }
}