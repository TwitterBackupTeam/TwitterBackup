using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TwitterBackup.Data.DTO;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.Repository;
using TwitterBackup.Data.Services.ServiceInterfaces;
using TwitterBackup.Data.Services.Utils;

namespace TwitterBackup.Data.Services
{
    public class TweetService : DatabaseService, ITweetService
    {
        private readonly IRepository<Tweet> tweetRepository;
        private readonly IRepository<Tweeter> tweeterRepository;

        public TweetService(IAutoMapper autoMapper, IWorkSaver workSaver, IRepository<Tweet> tweetRepository, IRepository<Tweeter> tweeterRepository) : base(autoMapper, workSaver)
        {
            this.tweetRepository = tweetRepository;
            this.tweeterRepository = tweeterRepository;
        }

        public TweetDTO GetTweetById(long id)
        {
            var tweet = this.tweetRepository.All().Include(t => t.Author).Where(t => t.Id == id).First();

            return this.AutoMapper.MapTo<TweetDTO>(tweet);
        }

        public async Task<bool> Add(TweetDTO dto)
        {
            if (this.tweetRepository.GetById(dto.Id) != null)
            {
                return true;
            }

            var tweet = this.AutoMapper.MapTo<Tweet>(dto);
            tweet.CreatedAt = DateTime.ParseExact(dto.CreatedAtStr, "ddd MMM dd HH:mm:ss K yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);

            if (this.tweeterRepository.All().FirstOrDefault(t => t.Id == tweet.Author.Id) != null)
            {
                tweet.Author = this.tweeterRepository.All().FirstOrDefault(t => t.Id == tweet.Author.Id);
            }

            this.tweetRepository.Add(tweet);
            var res = await this.WorkSaver.SaveChangesAsync();

            return res;
        }

        public async Task<bool> Delete(long id)
        {
            this.tweetRepository.Delete(this.tweetRepository.GetById(id));
            var res = await this.WorkSaver.SaveChangesAsync();

            return res;
        }
    }
}
