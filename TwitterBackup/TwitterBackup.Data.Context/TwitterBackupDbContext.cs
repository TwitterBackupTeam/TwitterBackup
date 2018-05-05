using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TwitterBackup.Data.Models;

namespace TwitterBackup.Data.Context
{
    public class TwitterBackupDbContext : IdentityDbContext<User>
    {
        public TwitterBackupDbContext(DbContextOptions<TwitterBackupDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserTweet>()
                .HasKey(t => new {t.UserId, t.TweetId});

            builder.Entity<Tweet>().Property(t => t.Id).ValueGeneratedNever();
            builder.Entity<Tweeter>().Property(t => t.Id).ValueGeneratedNever();
        }

        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<Tweeter> Tweeters { get; set; }
    }
}
