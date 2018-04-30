using TwitterBackup.Data.Services.Utils;

namespace TwitterBackup.Data.Services
{
    public class TwitterService
    {
        private ITwitterClient twitterClient;

        public TwitterService(ITwitterClient twitterClient)
        {
            this.twitterClient = twitterClient;
        }
    }
}
