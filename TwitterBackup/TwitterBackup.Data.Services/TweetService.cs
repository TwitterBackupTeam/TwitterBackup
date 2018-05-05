using System;
using System.Globalization;
using System.Threading.Tasks;
using TwitterBackup.Data.Context;
using TwitterBackup.Data.DTO;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.Repository;
using TwitterBackup.Data.Services.ServiceInterfaces;
using TwitterBackup.Data.Services.Utils;

namespace TwitterBackup.Data.Services
{
    public class TweetService : ITweetService
    {
        private readonly IRepository<Tweet> tweetRepository;
        private readonly IAutoMapper autoMapper;
        private readonly IWorkSaver workSaver;

        public TweetService(IRepository<Tweet> tweetRepository, IAutoMapper autoMapper, IWorkSaver workSaver)
        {
            this.tweetRepository = tweetRepository;
            this.autoMapper = autoMapper;
            this.workSaver = workSaver;
        }

        public TweetDTO GetTweetById(long id)
        {
            var tweet = this.tweetRepository.GetById(id);

            return this.autoMapper.MapTo<TweetDTO>(tweet);
        }

        public async Task<bool> Add(TweetDTO dto)
        {
            if (this.tweetRepository.GetById(dto.Id) != null)
            {
                throw new ArgumentException("This tweet is already added.");
            }

            var tweet = this.autoMapper.MapTo<Tweet>(dto);
            tweet.CreatedAt = DateTime.ParseExact(dto.CreatedAtStr, "ddd MMM dd HH:mm:ss K yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
            this.tweetRepository.Add(tweet);
            var res = await this.workSaver.SaveChangesAsync();

            return res;
        }

        public async Task<bool> Delete(long id)
        {
            this.tweetRepository.Delete(this.tweetRepository.GetById(id));
            var res = await this.workSaver.SaveChangesAsync();

            return res;
        }
    }
}
