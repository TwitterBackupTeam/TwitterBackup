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

        public TweetService(IRepository<Tweet> tweetRepository, IAutoMapper autoMapper, IUnitOfWork unitOfWork) : base(autoMapper, unitOfWork)
        {
            this.tweetRepository = tweetRepository;
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
                throw new ArgumentException("This tweet is already added.");
            }

            var tweet = this.AutoMapper.MapTo<Tweet>(dto);
            tweet.CreatedAt = DateTime.ParseExact(dto.CreatedAtStr, "ddd MMM dd HH:mm:ss K yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
            this.tweetRepository.Add(tweet);
            var res = await this.UnitOfWork.SaveChangesAsync();

            return res;
        }

        public async Task<bool> Delete(long id)
        {
            this.tweetRepository.Delete(this.tweetRepository.GetById(id));
            var res = await this.UnitOfWork.SaveChangesAsync();

            return res;
        }
    }
}
