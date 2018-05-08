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
        public TweetService(IAutoMapper autoMapper, IUnitOfWork unitOfWork) : base(autoMapper, unitOfWork)
        { }

        public TweetDTO GetTweetById(long id)
        {
            var tweet = this.UnitOfWork.TweetRepository.All().Include(t => t.Author).Where(t => t.Id == id).First();

            return this.AutoMapper.MapTo<TweetDTO>(tweet);
        }

        public async Task<bool> Add(TweetDTO dto)
        {
            if (this.UnitOfWork.TweetRepository.GetById(dto.Id) != null)
            {
                return true;
            }

            var tweet = this.AutoMapper.MapTo<Tweet>(dto);
            tweet.CreatedAt = DateTime.ParseExact(dto.CreatedAtStr, "ddd MMM dd HH:mm:ss K yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
            if (this.UnitOfWork.TweeterRepository.All().Where(t => t.Id == dto.Author.Id) != null)
            {
                tweet.Author = this.UnitOfWork.TweeterRepository.All().First(t => t.Id == dto.Author.Id);
            }

            this.UnitOfWork.TweetRepository.Add(tweet);
            var res = await this.UnitOfWork.SaveChangesAsync();

            return res;
        }

        public async Task<bool> Delete(long id)
        {
            this.UnitOfWork.TweetRepository.Delete(this.UnitOfWork.TweetRepository.GetById(id));
            var res = await this.UnitOfWork.SaveChangesAsync();

            return res;
        }
    }
}
